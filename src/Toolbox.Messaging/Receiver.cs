using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
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
            var listener = Listener.Create(connection, this);
            
            _listeners.Add(listener);

            if (Started)
                listener.Start();

            return listener;
        }

        private readonly List<Listener> _listeners = new();

        public IReadOnlyList<Listener> Listeners => _listeners;

        protected Dictionary<string, Delegate> Handlers { get; } = new Dictionary<string, Delegate>();

        public bool Started { get; private set; }

        private CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();
        internal CancellationToken Token => TokenSource.Token;

        /// <summary>
        /// Gets the first connection if it exists.
        /// </summary>
        internal string? DefaultConnection => _listeners.FirstOrDefault()?.Connection;

        public void Start()
        {
            if (Started) return;

            TokenSource.Cancel();
            TokenSource = new CancellationTokenSource();

            _listeners.ForEach(l => l.Start());

            Started = true;
        }

        public void Stop()
        {
            if (!Started) return;

            TokenSource.Cancel();
            _listeners.ForEach(l => l.Stop());

            Started = false;
        }

        internal void DoReceive(MemoryStream stream)
        {
            var formatter = new BinaryFormatter();
            var message = (Message)formatter.Deserialize(stream);

            Trace.WriteLine($"message '{message.Name}' (arguments[{message.Arguments.Count}]) received", nameof(Receiver));

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
            if (!Handlers.TryGetValue(message.Name, out var messageHandler))
            {
                Trace.WriteLine($"No handler for message '{message.Name}", GetType().Name);
                return;
            }

            Trace.WriteLine($"invoking '{messageHandler.Method.Name}()'", GetType().Name);
            messageHandler.DynamicInvoke(message.Arguments.ToArray());
        }
    }
}
