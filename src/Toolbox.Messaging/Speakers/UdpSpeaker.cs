using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using Toolbox.Messaging.Schemes;

namespace Toolbox.Messaging.Speakers
{
    [Scheme("udp")]
    class SenderUdp : SocketSpeaker
    {
        public SenderUdp(string connection) : base(connection)
        {
            Client = new UdpClient();
        }

        private UdpClient Client { get; }

        protected override void Send(MemoryStream stream)
        {
            Trace.WriteLine($"send - {stream.Length} bytes", TraceCategory);

            var buffer = stream.ToArray();
            var send = Client.Send(buffer, buffer.Length, EndPoint);

            Trace.WriteLine($"sent - {send} bytes", TraceCategory);
        }
    }
}
