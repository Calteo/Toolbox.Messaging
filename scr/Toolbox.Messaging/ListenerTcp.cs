using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Toolbox.Messaging
{
    [Scheme("tcp")]
    class ListenerTcp : ListenerSocket
    {
        public ListenerTcp(Uri uri) : base(uri, SocketType.Stream, ProtocolType.Tcp)
        {
            Listener = new TcpListener(EndPoint);
        }

        private TcpListener Listener { get; }

        internal override async void Start()
        {
            Listener.Start();

            while (true)
            {
                try
                {
                    Trace.WriteLine("awaiting connection", "ListenerTcp.Start");
                    TcpClient tcpClient = await Listener.AcceptTcpClientAsync();
                    Trace.WriteLine($"got connection - {tcpClient.Client.RemoteEndPoint}", "ListenerTcp.Start");
                    var task = StartHandleConnectionAsync(tcpClient);

                    // if already faulted, re-throw any error on the calling context
                    if (task.IsFaulted)
                        task.Wait();
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message, "ListenerTcp.Start");
                }
            }
        }

        private async Task StartHandleConnectionAsync(TcpClient tcpClient)
        {
            // start the new connection task
            var connectionTask = HandleConnectionAsync(tcpClient);

            // catch all errors of HandleConnectionAsync
            try
            {
                // we may be on another thread after "await"
                await connectionTask;
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message, "ListenerTcp.StartHandleConnectionAsync");
            }
        }

        private async Task HandleConnectionAsync(TcpClient tcpClient)
        {
            await Task.Yield();

            // continue asynchronously on another threads

            var clientEndPoint = tcpClient.Client.RemoteEndPoint;
            try
            {
                var networkStream = tcpClient.GetStream();
                var lengthBuffer = new byte[sizeof(long)];
                while (true)
                {
                    Trace.WriteLine($"{clientEndPoint} - awaiting request", "ListenerTcp.HandleConnectionAsync");

                    var length = await networkStream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length);
                    if (length == lengthBuffer.Length)
                    {
                        var messageLength = BitConverter.ToInt64(lengthBuffer, 0);
                        var messageBuffer = new byte[messageLength];
                        var gotLength = await networkStream.ReadAsync(messageBuffer, 0, messageBuffer.Length);

                        Trace.WriteLine($"{clientEndPoint} - message read - {gotLength} bytes", "ListenerTcp.HandleConnectionAsync");

                        if (gotLength == messageLength)
                        {
                            var stream = new MemoryStream(messageBuffer);
                            var formatter = new BinaryFormatter();
                            var message = (Message)formatter.Deserialize(stream);

                            Trace.WriteLine($"{clientEndPoint} - message '{message.Name}' received", "ListenerTcp.HandleConnectionAsync");

                            Receiver.DoReceive(message);
                        }
                    }
                    else
                    {
                        Trace.WriteLine($"{clientEndPoint} - bad length input (length={length})", "ListenerTcp.HandleConnectionAsync");
                        break; // Client closed connection
                    }
                }
                Trace.WriteLine($"{clientEndPoint} - close", "ListenerTcp.HandleConnectionAsync");
                tcpClient.Close();
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{clientEndPoint} - " + exception.Message, "ListenerTcp.Process");

                if (tcpClient.Connected)
                {
                    Trace.WriteLine($"{clientEndPoint} - close@catch", "ListenerTcp.Process");
                    tcpClient.Close();
                }
            }
        }

        internal override void Stop()
        {
            Listener.Stop();
        }
    }
}
