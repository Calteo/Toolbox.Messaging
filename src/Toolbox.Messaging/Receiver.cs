using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Toolbox.Messaging.Listeners;

namespace Toolbox.Messaging
{
    public class Receiver 
    {
        public Receiver(params string[] connections)
        {
            var type = GetType();

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                    .Where(p => p.ReturnType == typeof(void)))
            {
                var attribute = method.GetCustomAttribute<MessageHandlerAttribute>();
                if (attribute != null)
                {
                    if (Handlers.ContainsKey(attribute.Name))
                        throw new ArgumentException($"Message '{attribute.Name}' already registered.");

                    var handler = method.CreateDelegate(
                                    Expression.GetDelegateType(method.GetParameters()
                                    .Select(p => p.ParameterType).Concat(new[] { method.ReturnType }).ToArray()
                                    ), this);

                    Handlers.Add(attribute.Name, handler);
                }
            }

            foreach (var connection in connections)
            {
                AddListener(connection);
            }
        }

        public Listener AddListener(string connection)
        {
            var listener = Listener.Create(connection);
            listener.Receiver = this;

            _listeners.Add(listener);

            if (Started)
                listener.Start();

            return listener;
        }

        private readonly List<Listener> _listeners = new List<Listener>();

        public IReadOnlyList<Listener> Listeners => _listeners;

        protected Dictionary<string, Delegate> Handlers { get; } = new Dictionary<string, Delegate>();

        public bool Started { get; private set; }

        public void Start()
        {
            if (Started) return;

            _listeners.ForEach(l => l.Start());

            Started = true;
        }

        public void Stop()
        {
            if (!Started) return;

            _listeners.ForEach(l => l.Stop());

            Started = false;
        }

        internal void DoReceive(Message message)
        {
            for (var i = 0; i < message.Arguments.Count; i++)
            {
                if (message.Arguments[i] is EndPoint endpoint)
                {
                    message.Arguments[i] = new Sender(endpoint.Connection);
                }
            }

            OnReceived(message);
            InvokeHandler(message);                        
        }

        protected virtual void OnReceived(Message message)
        {
        }

        protected virtual void InvokeHandler(Message message)
        {
            if (!Handlers.TryGetValue(message.Name, out Delegate messageHandler))
            {
                Trace.WriteLine($"No handler for message '{message.Name}", GetType().Name);
                return;
            }

            Trace.WriteLine($"invoking '{messageHandler.Method.Name}'", GetType().Name);
            messageHandler.DynamicInvoke(message.Arguments.ToArray());
        }
    }
}
