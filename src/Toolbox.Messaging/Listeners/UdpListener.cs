using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Toolbox.Messaging.Schemes;

namespace Toolbox.Messaging.Listeners
{
    [Scheme("udp")]
    class UdpListener : SocketListener
    {
        public UdpListener(string connection) : base(connection, SocketType.Dgram, ProtocolType.Udp)
        {
            Listener = new UdpClient(EndPoint);
        }
        private UdpClient Listener { get; }

        internal override async void Start()
        {
            var tc = $"{nameof(UdpListener)}[{Connection}]";

            while (true)
            {
                try
                {
                    Trace.WriteLine($"awaiting message - {Connection}", tc);
                    var result = await Listener.ReceiveAsync();
                    Trace.WriteLine($"got message - {result.RemoteEndPoint}", tc);
                    var task = HandleMessageAsync(result);

                    // if already faulted, re-throw any error on the calling context
                    if (task.IsFaulted)
                        task.Wait();
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message, tc);
                }
            }
        }

        internal override void Stop()
        {
            throw new NotImplementedException();
        }

        private async Task HandleMessageAsync(UdpReceiveResult result)
        {
            await Task.Yield();

            // continue asynchronously on another threads

            try
            {
                var clientEndPoint = result.RemoteEndPoint;

                Trace.WriteLine($"read - {result.Buffer.Length} bytes", TraceCategory);

                DoReceiveASync(result.Buffer);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message, TraceCategory);
            }
        }
    }
}