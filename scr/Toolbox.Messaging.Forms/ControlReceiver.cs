using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Messaging.Forms
{
    /// <summary>
    /// A <see cref="Reciever"/> for <see cref="Control"/>s.
    /// </summary>
    /// <remarks>
    /// All messages are automatically processed on the UI thread.
    /// </remarks>
    public class ControlReceiver : Receiver
    {
        public ControlReceiver(Control control)
        {
            Control = control;
            Handler = new Action<Message>(OnReceived);
        }

        private Control Control { get;  }
        private Action<Message> Handler { get; }

        protected override void OnReceived(Message message)
        {
            if (Control.InvokeRequired)
                Control.BeginInvoke(Handler, message);
            else
                base.OnReceived(message);
        }
    }
}
