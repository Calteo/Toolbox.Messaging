using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                TextBox.SelectionStart = TextBox.Text.Length;
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
