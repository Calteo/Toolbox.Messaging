using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Toolbox.Messaging
{
    [Scheme("udp")]
    public class ListenerUdp : ListenerSocket
    {
        public ListenerUdp(Uri uri) : base(uri, SocketType.Dgram, ProtocolType.Udp)
        {
            Listener = new UdpClient(EndPoint);
        }
        private UdpClient Listener { get; }

        internal override async void Start()
        {
            while (true)
            {
                try
                {
                    Trace.WriteLine($"awaiting message - {Listener}", "ListenerUdp.Start");
                    var result = await Listener.ReceiveAsync();
                    Trace.WriteLine($"got message - {result.RemoteEndPoint}", "ListenerUdp.Start");
                    var task = HandleMessageAsync(result);

                    // if already faulted, re-throw any error on the calling context
                    if (task.IsFaulted)
                        task.Wait();
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message, "ListenerUdp.Start");
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

                Trace.WriteLine($"{clientEndPoint} - message read - {result.Buffer.Length} bytes", "ListeneUdp.HandleMessageAsync");

                var stream = new MemoryStream(result.Buffer);
                var formatter = new BinaryFormatter();
                var message = (Message)formatter.Deserialize(stream);

                Trace.WriteLine($"{clientEndPoint} - message '{message.Name}' received", "ListenerUdp.HandleMessageAsync");

                Receiver.DoReceive(message);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message, "ListenerUdp.HandleMessageAsync");
            }
        }
    }
}