using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace Toolbox.Messaging
{
    [Scheme("udp")]
    public class SenderUdp : SenderSocket
    {
        public SenderUdp(Uri uri) : base(uri)
        {
            Client = new UdpClient();
        }

        private UdpClient Client { get; }

        internal override void Post(MemoryStream stream)
        {
            Trace.WriteLine($"{Uri} - post - {stream.Length} bytes");

            var buffer = stream.ToArray();
            var send = Client.Send(buffer, buffer.Length, EndPoint);

            Trace.WriteLine($"{Uri} - posted - {send} bytes");
        }
    }
}
