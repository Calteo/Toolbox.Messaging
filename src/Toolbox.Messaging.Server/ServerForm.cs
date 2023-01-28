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

            Receiver = new ServerReceiver(this, DemoApplication.TcpConnection, DemoApplication.UdpConnection);
        }

        public Receiver Receiver { get; set; }

        private void ServerFormLoad(object sender, System.EventArgs e)
        {
            textBoxTcp.Text = Receiver.Listeners[0].Connection;
            textBoxUdp.Text = Receiver.Listeners[1].Connection;
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
            buttonStart.PerformClick();
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

        private void ButtonStartClick(object sender, EventArgs e)
        {
            Receiver.Start();
            AppendMessage("Reciever started");
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }

        private void ButtonStopClick(object sender, EventArgs e)
        {
            Receiver.Stop();
            AppendMessage("Reciever stopped");
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }
    }
}