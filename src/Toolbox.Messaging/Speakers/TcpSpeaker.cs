using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using Toolbox.Messaging.Schemes;

namespace Toolbox.Messaging.Speakers
{
    [Scheme("tcp")]
    class SenderTcp : SocketSpeaker
    {
        public SenderTcp(string connection) : base(connection)
        {            
            Client = new TcpClient(Uri.Host, Uri.Port);
        }

        private TcpClient Client { get; }
        
        protected override void Send(MemoryStream stream)
        {
            Trace.WriteLine($"send - {stream.Length} bytes", TraceCategory);

            if (!Client.Connected)
                throw new Exception("Not connected");

            stream.Position = 0;

            var lengthBuffer = BitConverter.GetBytes(stream.Length);

            var tcpStream = Client.GetStream();
            tcpStream.Write(lengthBuffer, 0, lengthBuffer.Length);
            stream.WriteTo(tcpStream);
            tcpStream.Flush();

            Trace.WriteLine($"sent - {stream.Length} bytes", TraceCategory);
        }
    }
}
