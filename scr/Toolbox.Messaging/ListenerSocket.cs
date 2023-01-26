using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Toolbox.Messaging
{
    public abstract class ListenerSocket : Listener
    {
        private static readonly IPEndPoint DefaultLoopbackEndpoint = new IPEndPoint(IPAddress.Loopback, 0);

        protected ListenerSocket(Uri uri, SocketType socketType, ProtocolType protocolType) : base(uri)
        {
            if (uri.LocalPath != "/")
                throw new ArgumentException($"Invalid local path on connection ({uri.OriginalString}).", nameof(uri));

            if (IPAddress.TryParse(uri.Host, out IPAddress address))
                throw new ArgumentException($"Invalid address on connection ({uri.OriginalString}).", nameof(uri));

            if (address == null)
            {
                address = Dns.GetHostAddresses(uri.Host).FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            }

            if (address == null)
                throw new ArgumentException($"Can not resolve address on connection ({uri.OriginalString}).", nameof(uri));

            if (uri.IsDefaultPort)
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, socketType, protocolType))
                {
                    socket.Bind(DefaultLoopbackEndpoint);
                    var port = ((IPEndPoint)socket.LocalEndPoint).Port;

                    var builder = new UriBuilder(uri)
                    {
                        Port = port
                    };
                    Uri = builder.Uri;
                }
            }
            EndPoint = new IPEndPoint(address, Uri.Port);
        }

        public IPEndPoint EndPoint { get; private set; }
    }
}
