using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
        /// <exception cref="ArgumentException">
        /// Is thrown if the connection can not be converted to a valid endpoint.
        /// </exception>
        protected SocketListener(string connection, Receiver receiver, SocketType socketType, ProtocolType protocolType) : base(connection, receiver)
        {
            var uri = new Uri(connection);

            if (uri.LocalPath != "/")
                throw new ArgumentException($"Invalid local path on connection ({connection}).", nameof(connection));

            if (IPAddress.TryParse(uri.Host, out var address))
                throw new ArgumentException($"Invalid address on connection ({connection}).", nameof(connection));

            address ??= Dns.GetHostAddresses(uri.Host).FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            if (address == null)
                throw new ArgumentException($"Can not resolve address on connection ({connection}).", nameof(connection));

            var port = uri.Port;

            if (uri.IsDefaultPort)
            {
                using var socket = new Socket(AddressFamily.InterNetwork, socketType, protocolType);

                socket.Bind(DefaultLoopbackEndpoint);
                if (socket.LocalEndPoint is IPEndPoint ipEndPoint)
                {
                    port = ipEndPoint.Port;
                }
                else
                {
                    throw new InvalidCastException($"Socket for listener {Connection} has no {nameof(IPEndPoint)}.");
                }

                var builder = new UriBuilder(uri)
                {
                    Port = port
                };
                Connection = builder.Uri.ToString();
            }
            EndPoint = new IPEndPoint(address, port);
        }

        /// <summary>
        /// Get <see cref="EndPoint"/> that this <see cref="SocketListener"/> is bound to.
        /// </summary>
        public IPEndPoint EndPoint { get; private set; }
    }
}