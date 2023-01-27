using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Toolbox.Messaging.Listeners
{
    /// <summary>
    /// Listener on a <see cref="Socket"/>.
    /// </summary>
    abstract class SocketListener : Listener
    {
        private static readonly IPEndPoint DefaultLoopbackEndpoint = new IPEndPoint(IPAddress.Loopback, 0);

        /// <summary>
        /// Initialize a new instance of the <see cref="SocketListener"/> class.
        /// </summary>
        /// <param name="uri">Endpoint to listen to.</param>
        /// <param name="socketType">Type of socket.</param>
        /// <param name="protocolType">Type of protocol</param>
        /// <exception cref="ArgumentException"></exception>
        protected SocketListener(string connection, SocketType socketType, ProtocolType protocolType) : base(connection)
        {
            var uri = new Uri(connection);

            if (uri.LocalPath != "/")
                throw new ArgumentException($"Invalid local path on connection ({uri.OriginalString}).", nameof(uri));

            if (IPAddress.TryParse(uri.Host, out IPAddress address))
                throw new ArgumentException($"Invalid address on connection ({uri.OriginalString}).", nameof(uri));

            address ??= Dns.GetHostAddresses(uri.Host).FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            if (address == null)
                throw new ArgumentException($"Can not resolve address on connection ({uri.OriginalString}).", nameof(uri));

            var port = uri.Port;

            if (uri.IsDefaultPort)
            {
                using var socket = new Socket(AddressFamily.InterNetwork, socketType, protocolType);

                socket.Bind(DefaultLoopbackEndpoint);
                port = ((IPEndPoint)socket.LocalEndPoint).Port;

                var builder = new UriBuilder(uri)
                {
                    Port = port
                };
                Connection = builder.Uri.ToString();
            }
            EndPoint = new IPEndPoint(address, port);
        }

        public IPEndPoint EndPoint { get; private set; }
    }
}