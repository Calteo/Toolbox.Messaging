using System.IO;
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
        protected Listener(string connection, Receiver receiver) : base(connection)
        {            
            Receiver = receiver;
        }

        /// <summary>
        /// The receiver containing this listener.
        /// </summary>
        public Receiver Receiver { get; }

        /// <summary>
        /// Start listening on the endpoint
        /// </summary>
        internal abstract void Start();

        /// <summary>
        /// Stop listening on the Endpoint
        /// </summary>
        internal abstract void Stop();

        /// <summary>
        /// Starts a new <see cref="Task"/> to handle a <see cref="Message"/>.
        /// </summary>
        /// <param name="buffer"></param>
        protected async void DoReceiveASync(byte[] buffer)
        {
            await Task.Run(() => DoReceive(buffer));
        }

        private void DoReceive(byte[] buffer)
        {            
            var stream = new MemoryStream(buffer);

            var decoded = Decode(stream);

            Receiver.DoReceive(decoded);
        }

        /// <summary>
        /// Optional decoding methof before the <see cref="Message"/> is deserialized.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected virtual MemoryStream Decode(MemoryStream stream)
        {
            return stream;
        }
    }
}