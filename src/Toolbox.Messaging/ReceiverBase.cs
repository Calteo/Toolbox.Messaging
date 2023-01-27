using System;
using System.Collections.Generic;
using System.Diagnostics;
using Toolbox.Messaging.Listeners;

namespace Toolbox.Messaging
{
    public class ReceiverBase
    {
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
            OnReceived(message);
        }

        protected virtual void OnReceived(Message message)
        {
            if (!Handlers.TryGetValue(message.Name, out Delegate messageHandler))
            {
                Trace.WriteLine($"No handler for message '{message.Name}", nameof(ReceiverBase));
                return;
            }

            for (var i = 0; i < message.Arguments.Count; i++)
            {
                if (message.Arguments[i] is EndPoint endpoint)
                {
                    message.Arguments[i] = new Sender(endpoint.Connection);
                }
            }

            messageHandler.DynamicInvoke(message.Arguments.ToArray());
        }
    }
}
