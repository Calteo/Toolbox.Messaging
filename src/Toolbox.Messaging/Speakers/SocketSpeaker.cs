using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Toolbox.Messaging.Speakers
{
    abstract class SocketSpeaker : Speaker
    {
        internal SocketSpeaker(string connection) : base(connection)
        {
            Uri = new Uri(connection);

            if (Uri.LocalPath != "/")
                throw new ArgumentException($"Invalid local path on connection ({connection}).", nameof(connection));

            if (Uri.Host == "")
                throw new ArgumentException($"No host given on connection ({connection}).", nameof(connection));

            if (Uri.IsDefaultPort)
                throw new ArgumentException($"No port given on connection ({connection}).", nameof(connection));

            if (IPAddress.TryParse(Uri.Host, out var address))
                throw new ArgumentException($"Invalid address on connection ({connection}).", nameof(connection));

            address ??= Dns.GetHostAddresses(Uri.Host).FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            if (address == null)
                throw new ArgumentException($"Can not resolve address on connection ({connection}).", nameof(connection));

            EndPoint = new IPEndPoint(address, Uri.Port);
        }

        public Uri Uri { get; }
        public IPEndPoint EndPoint { get; private set; }
    }
}
