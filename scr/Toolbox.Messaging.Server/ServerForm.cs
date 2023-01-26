using System.Net;
using Toolbox.Messaging.Forms;

namespace Toolbox.Messaging.Server
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();

            Receiver = new ServerReceiver(this);
        }

        public ReceiverBase Receiver { get; set; }

        private void ServerFormLoad(object sender, System.EventArgs e)
        {
            // use a fixed port
            textBoxTcp.Text = Receiver.AddListener($"tcp://{Dns.GetHostName()}:55833").Uri.ToString();
            // use a free port
            textBoxUdp.Text = Receiver.AddListener($"udp://{Dns.GetHostName()}").Uri.ToString();

            Receiver.Start();
        }

        private void AppendMessage(string text)
        {            
            textBoxMessages.Text += $"{DateTime.Now:HH:mm:ss}: {text}" + Environment.NewLine;
            textBoxMessages.SelectionLength = 0;
            textBoxMessages.SelectionStart = text.Length;
            textBoxMessages.ScrollToCaret();
        }

        private void ServerFormShown(object sender, EventArgs e)
        {
            Receiver.Start();

            AppendMessage("server listening");
        }

        public void AddHello(string text)
        {
            AppendMessage($"Hello '{text}'");
        }

        public void SayHello(string text)
        {
            AppendMessage($"SayHello '{text}'");

            MessageBox.Show(this, $"Say hello to {text}.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal void GotData(MessageData data)
        {
            AppendMessage($"GotData {data.MagicNumber}");
            data.ResposeNumber = 10000 - data.MagicNumber;
            AppendMessage($"ReplyData {data.ResposeNumber}");
        }
    }
}