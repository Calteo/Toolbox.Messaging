using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using Toolbox.Messaging.Schemes;

namespace Toolbox.Messaging.Listeners
{
    /// <summary>
    /// Listener on a TCP port.
    /// </summary>
    [Scheme("tcp")]
    class TcpListener : SocketListener
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="TcpListener"/> class.
        /// </summary>
        /// <param name="connection"></param>
        public TcpListener(string connection, Receiver receiver) : base(connection, receiver, SocketType.Stream, ProtocolType.Tcp)
        {            
        }

        private System.Net.Sockets.TcpListener? Listener { get; set; }

        /// <inheritdoc />
        internal override async void Start()
        {
            Listener = new System.Net.Sockets.TcpListener(EndPoint);
            Listener.Start();

            while (!Receiver.Token.IsCancellationRequested)
            {
                try
                {
                    Trace.WriteLine("awaiting connection", TraceCategory);
                    TcpClient tcpClient = await Listener.AcceptTcpClientAsync(Receiver.Token);
                    Trace.WriteLine($"got connection from {tcpClient.Client.RemoteEndPoint}", TraceCategory);
                    var task = StartHandleConnectionAsync(tcpClient);

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
                    Trace.WriteLine(exception.Message, TraceCategory);
                }
            }
        }

        /// <inheritdoc />
        internal override void Stop()
        {
            Listener?.Stop();
            Listener = null;
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
                    Trace.WriteLine($"awaiting request", TraceCategory);

                    var length = await networkStream.ReadAsync(lengthBuffer, 0, lengthBuffer.Length, Receiver.Token);
                    if (length == lengthBuffer.Length)
                    {
                        var messageLength = BitConverter.ToInt64(lengthBuffer, 0);

                        Trace.WriteLine($"got {messageLength} bytes", TraceCategory);

                        var messageBuffer = new byte[messageLength];
                        var gotLength = await networkStream.ReadAsync(messageBuffer, 0, messageBuffer.Length, Receiver.Token);

                        if (gotLength == messageLength)
                        {                           
                            DoReceiveASync(messageBuffer);
                        }
                        else
                        {
                            Trace.WriteLine($"read {gotLength} <> {messageLength} bytes", TraceCategory);
                        }
                    }
                    else
                    {
                        Trace.WriteLine($"bad length on input ({length}<>{lengthBuffer.Length})", TraceCategory);
                        break; // Client closed connection
                    }
                }
                Trace.WriteLine($"{clientEndPoint} - close", TraceCategory);
                tcpClient.Close();
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message, TraceCategory);

                if (tcpClient.Connected)
                {
                    Trace.WriteLine($"close@catch", TraceCategory);
                    tcpClient.Close();
                }
            }
        }
    }
}
