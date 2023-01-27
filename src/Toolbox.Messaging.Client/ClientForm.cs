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
        }

        private void ClientFormLoad(object sender, EventArgs e)
        {
            textBoxServer.Text = $"tcp://{Dns.GetHostName()}:55833";            
        }

        private Sender? Sender { get; set; }
        public ReceiverBase Receiver { get; }

        private void ButtonPostHelloClick(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;

                AppendMessage($"post hello '{textBoxSayHello.Text}'");
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

        private void ButtonConnectClick(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Sender = new Sender(textBoxServer.Text);

                buttonPostHello.Enabled = 
                buttonPostSayHello.Enabled = 
                buttonPostData.Enabled = true;

                AppendMessage($"connected - {textBoxServer.Text}");
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

            AppendMessage($"receiver - {Receiver.Listeners[0].Connection}");
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