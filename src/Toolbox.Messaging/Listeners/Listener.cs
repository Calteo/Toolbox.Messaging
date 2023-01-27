using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public ReceiverBase Receiver { get; internal set; }

        internal abstract void Start();

        internal abstract void Stop();
    }
}