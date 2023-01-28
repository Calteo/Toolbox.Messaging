using System.Net;

namespace Toolbox.Messaging.Forms
{
    public static class DemoApplication
    {
        public static string TcpConnection => $"tcp://{Dns.GetHostName()}:55833";
        public static string UdpConnection => $"udp://{Dns.GetHostName()}:55834";
    }
}
