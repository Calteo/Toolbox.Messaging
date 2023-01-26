using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Toolbox.Messaging
{
    public abstract class SenderSocket : Sender
    {
        internal SenderSocket(Uri uri) : base(uri)
        {
            if (uri.LocalPath != "/")
                throw new ArgumentException($"Invalid local path on connection ({uri.OriginalString}).", nameof(uri));

            if (uri.Host == "")
                throw new ArgumentException($"No host given on connection ({uri.OriginalString}).", nameof(uri));

            if (uri.IsDefaultPort)
                throw new ArgumentException($"No port given on connection ({uri.OriginalString}).", nameof(uri));

            if (IPAddress.TryParse(uri.Host, out IPAddress address))
                throw new ArgumentException($"Invalid address on connection ({uri.OriginalString}).", nameof(uri));

            if (address == null)
            {
                address = Dns.GetHostAddresses(uri.Host).FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            }

            if (address == null)
                throw new ArgumentException($"Can not resolve address on connection ({uri.OriginalString}).", nameof(uri));


            EndPoint = new IPEndPoint(address, uri.Port);
        }

        public IPEndPoint EndPoint { get; private set; }
    }
}
