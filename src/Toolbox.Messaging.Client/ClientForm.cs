using System.Diagnostics;
using System.Net;
using Toolbox.Messaging.Forms;

namespace Toolbox.Messaging.Client
{
    internal partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();

            Trace.Listeners.Add(new TextBoxTraceListener(textBoxMessages));

            // client reply recievers should use free ports
            Receiver = new ClientReceiver(this, $"udp://{Dns.GetHostName()}");

            contextMenuServer.Items.Add(DemoApplication.TcpConnection).Click += ServerConnectionClicked;
            contextMenuServer.Items.Add(DemoApplication.UdpConnection).Click += ServerConnectionClicked;
        }

        private void ServerConnectionClicked(object? sender, EventArgs e)
        {
            if (sender is ToolStripItem item)
            {
                textBoxServer.Text = item.Text;
                buttonUse.PerformClick();
            }                    
        }

        private void ClientFormLoad(object sender, EventArgs e)
        {
        }

        private Sender? Sender { get; set; }
        public Receiver Receiver { get; }

        private void ButtonPostHelloClick(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;

                AppendMessage($"post hello '{textBoxHello.Text}'");
                Sender?.Post("hello", textBoxHello.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ButtonUseClick(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Sender = new Sender(textBoxServer.Text);

                buttonPostHello.Enabled = 
                buttonPostSayHello.Enabled = 
                buttonPostData.Enabled = true;

                AppendMessage($"using - {Sender.Speaker.Connection}");
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                Sender = null;

                buttonPostHello.Enabled =
                buttonPostSayHello.Enabled =
                buttonPostData.Enabled = false;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AppendMessage(string text)
        {
            textBoxMessages.Text += text + Environment.NewLine;
            textBoxMessages.SelectionLength = 0;
            textBoxMessages.SelectionStart = text.Length;
            textBoxMessages.ScrollToCaret();
        }

        public void GotAnswer(string text)
        {
            MessageBox.Show(this, text, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            AppendMessage($"reply sayhello '{text}'");
        }

        private void ButtonPostSayHelloClick(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;

                AppendMessage($"post sayhello '{textBoxSayHello.Text}'");
                Sender?.Post("sayhello", textBoxSayHello.Text, Receiver);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ClientFormShown(object sender, EventArgs e)
        {
            Receiver.Start();
            AppendMessage($"receiver started - {Receiver.Listeners[0].Connection}");

            contextMenuServer.Items[1].PerformClick();
        }

        private void ButtonPostDataClick(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;

                var data = new MessageData();

                AppendMessage($"post data {data.MagicNumber}");
                Sender?.Post("data", data, Receiver);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        internal void GotDataReply(MessageData data)
        {
            AppendMessage($"reply data {data.MagicNumber} -> {data.ResposeNumber}");
        }
    }
}