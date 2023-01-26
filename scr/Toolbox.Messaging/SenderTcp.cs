using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace Toolbox.Messaging
{
    [Scheme("tcp")]
    public class SenderTcp : SenderSocket
    {
        public SenderTcp(Uri uri) : base(uri)
        {
            Client = new TcpClient(uri.Host, uri.Port);
        }

        private TcpClient Client { get; }

        internal override void Post(MemoryStream stream)
        {
            Trace.WriteLine($"{Uri} - post - {stream.Length} bytes");

            if (!Client.Connected)
                throw new Exception("Not connected");

            stream.Position = 0;

            var lengthBuffer = BitConverter.GetBytes(stream.Length);

            var tcpStream = Client.GetStream();
            tcpStream.Write(lengthBuffer, 0, lengthBuffer.Length);
            stream.WriteTo(tcpStream);
            tcpStream.Flush();

            Trace.WriteLine($"{Uri} - posted - {stream.Length} bytes");
        }
    }
}
