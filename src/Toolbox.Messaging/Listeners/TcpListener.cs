using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Toolbox.Messaging.Schemes;

namespace Toolbox.Messaging.Listeners
{
    [Scheme("tcp")]
    class TcpListener : SocketListener
    {
        public TcpListener(string connection) : base(connection, SocketType.Stream, ProtocolType.Tcp)
        {
            Listener = new System.Net.Sockets.TcpListener(EndPoint);
        }

        private System.Net.Sockets.TcpListener Listener { get; }

        internal override async void Start()
        {          
            Listener.Start();

            while (true)
            {
                try
                {
                    Trace.WriteLine("awaiting connection", TraceCategory);
                    TcpClient tcpClient = await Listener.AcceptTcpClientAsync();
                    Trace.WriteLine($"got connection - {tcpClient.Client.RemoteEndPoint}", TraceCategory);
                    var task = StartHandleConnectionAsync(tcpClient);

                    // if already faulted, re-throw any error on the calling context
                    if (task.IsFaulted)
                        task.Wait();
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception.Message, TraceCategory);
                }
            }
        }

        private async Task StartHandleConnectionAsync(TcpClient tcpClient)
        {
            var tc = $"{nameof(TcpListener)}[{Connection}]";

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
                Trace.WriteLine(exception.Message, tc);
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
                    Trace.WriteLine($"{clientEndPoint} - awaiting request", TraceCategory);

                    var length = await networkStream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length);
                    if (length == lengthBuffer.Length)
                    {
                        var messageLength = BitConverter.ToInt64(lengthBuffer, 0);
                        var messageBuffer = new byte[messageLength];
                        var gotLength = await networkStream.ReadAsync(messageBuffer, 0, messageBuffer.Length);

                        Trace.WriteLine($"{clientEndPoint} - message read - {gotLength} bytes", TraceCategory);

                        if (gotLength == messageLength)
                        {
                            var stream = new MemoryStream(messageBuffer);
                            var formatter = new BinaryFormatter();
                            var message = (Message)formatter.Deserialize(stream);

                            Trace.WriteLine($"{clientEndPoint} - message '{message.Name}' received", TraceCategory);

                            Receiver.DoReceive(message);
                        }
                    }
                    else
                    {
                        Trace.WriteLine($"{clientEndPoint} - bad length input (length={length})", TraceCategory);
                        break; // Client closed connection
                    }
                }
                Trace.WriteLine($"{clientEndPoint} - close", TraceCategory);
                tcpClient.Close();
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"{clientEndPoint} - " + exception.Message, TraceCategory);

                if (tcpClient.Connected)
                {
                    Trace.WriteLine($"{clientEndPoint} - close@catch", TraceCategory);
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
