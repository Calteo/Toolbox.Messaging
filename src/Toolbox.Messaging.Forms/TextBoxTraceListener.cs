using System.Diagnostics;

namespace Toolbox.Messaging.Forms
{
    public class TextBoxTraceListener : TraceListener
    {
        public TextBoxTraceListener(TextBox textBox)
        {
            TextBox = textBox;
            Handler = new Action<string?>(Append);
        }

        public TextBox TextBox { get; set; }
        private Action<string?> Handler { get; }

        private void Append(string? text)
        {
            if (TextBox.InvokeRequired)
                TextBox.BeginInvoke(Handler, text);
            else
            {
                TextBox.Text += $"> {text ?? "<null>"}" + Environment.NewLine;
                TextBox.SelectionLength = 0;
                TextBox.SelectionStart = TextBox.Text.Length-1;
                TextBox.ScrollToCaret();                
            }
        }

        public override void Write(string? message)
        {
            Append(message);
        }

        public override void WriteLine(string? message)
        {
            Append(message);
        }
    }
}
