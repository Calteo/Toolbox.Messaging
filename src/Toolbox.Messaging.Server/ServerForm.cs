using System.Diagnostics;
using System.Net;
using Toolbox.Messaging.Forms;

namespace Toolbox.Messaging.Server
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();

            Trace.Listeners.Add(new TextBoxTraceListener(textBoxMessages));

            Receiver = new ServerReceiver(this, $"tcp://{Dns.GetHostName()}:55833", $"udp://{Dns.GetHostName()}:55834");
        }

        public Receiver Receiver { get; set; }

        private void ServerFormLoad(object sender, System.EventArgs e)
        {
            textBoxTcp.Text = Receiver.Listeners[0].Connection;
            textBoxUdp.Text = Receiver.Listeners[0].Connection;

            Receiver.Start();
        }

        private void AppendMessage(string text)
        {            
            textBoxMessages.Text += text + Environment.NewLine;
            textBoxMessages.SelectionLength = 0;
            textBoxMessages.SelectionStart = text.Length;
            textBoxMessages.ScrollToCaret();
        }

        private void ServerFormShown(object sender, EventArgs e)
        {
            Receiver.Start();

            AppendMessage("server started");
        }

        public void AddHello(string text)
        {
            AppendMessage($"Hello '{text}'");
        }

        public bool SayHello(string text)
        {
            AppendMessage($"SayHello '{text}'");

            var rc = MessageBox.Show(this, $"Say hello to {text}.", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return rc == DialogResult.Yes;
        }

        internal void GotData(MessageData data)
        {
            AppendMessage($"GotData {data.MagicNumber}");
            data.ResposeNumber = 10000 - data.MagicNumber;
            AppendMessage($"ReplyData {data.ResposeNumber}");
        }
    }
}