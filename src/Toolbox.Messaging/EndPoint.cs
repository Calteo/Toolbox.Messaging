using System;

namespace Toolbox.Messaging
{
    /// <summary>
    /// Represents a connection.
    /// </summary>
    /// <remarks>
    /// This class is utilized to serialize <see cref="Listener"/> from a <see cref="Sender"/> to a <see cref="Receiver"/>.
    /// </remarks>
    [Serializable]
    class EndPoint
    {
        public EndPoint(string connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// The connection to transport
        /// </summary>
        public string Connection { get; }
    }
}
