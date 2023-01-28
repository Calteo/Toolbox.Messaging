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
        public UdpListener(string connection, Receiver receiver) : base(connection, receiver, SocketType.Dgram, ProtocolType.Udp)
        {            
        }

        private UdpClient? Listener { get; set; }

        internal override async void Start()
        {
            var tc = $"{nameof(UdpListener)}[{Connection}]";

            Listener = new UdpClient(EndPoint);

            while (!Receiver.Token.IsCancellationRequested)
            {
                try
                {
                    Trace.WriteLine($"awaiting message on {Connection}", tc);
                    var result = await Listener.ReceiveAsync(Receiver.Token);
                    Trace.WriteLine($"got message from {result.RemoteEndPoint}", tc);
                    var task = HandleMessageAsync(result);

                    // if already faulted, re-throw any error on the calling context
                    if (task.IsFaulted)
                        task.Wait();
                }
                catch (OperationCanceledException cancelException)
                {
                    Trace.WriteLine(cancelException.Message, TraceCategory);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message, tc);
                }
            }
        }

        internal override void Stop()
        {
            Listener?.Close();
            Listener = null;
        }

        private async Task HandleMessageAsync(UdpReceiveResult result)
        {
            await Task.Yield();

            // continue asynchronously on another threads

            try
            {
                var clientEndPoint = result.RemoteEndPoint;

                Trace.WriteLine($"got {result.Buffer.Length} bytes", TraceCategory);

                DoReceiveASync(result.Buffer);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message, TraceCategory);
            }
        }
    }
}