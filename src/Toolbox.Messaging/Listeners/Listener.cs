using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Toolbox.Messaging.Schemes;

namespace Toolbox.Messaging.Listeners
{
    /// <summary>
    /// Abstract class for all listeners.
    /// </summary>
    public abstract class Listener : SchemedConnection<Listener>
    {
        /// <summary>
        /// Intiailzes a new instance of the <see cref="Listener"/> class for an endpoint.
        /// </summary>
        /// <param name="connection">The endpoint to listen.</param>
        protected Listener(string connection) : base(connection)
        {            
        }

        /// <summary>
        /// The receiver containing this listener.
        /// </summary>
        public Receiver Receiver { get; internal set; }

        internal abstract void Start();

        internal abstract void Stop();

        protected async void DoReceiveASync(byte[] buffer)
        {
            await Task.Run(() => DoReceive(buffer));
        }

        private void DoReceive(byte[] buffer)
        {            
            var stream = new MemoryStream(buffer);

            var decoded = Decode(stream);
            
            var formatter = new BinaryFormatter();            
            var message = (Message)formatter.Deserialize(decoded);

            Trace.WriteLine($"message '{message.Name}' received", TraceCategory);
            Receiver.DoReceive(message);            
        }

        protected virtual MemoryStream Decode(MemoryStream stream)
        {
            return stream;
        }
    }
}