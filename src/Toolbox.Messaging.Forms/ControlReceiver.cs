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
        public ControlReceiver(Control control, params string[] connections) : base(connections)
        {
            Control = control;
            Handler = new Action<Message>(InvokeHandler);
        }

        private Control Control { get;  }
        private Action<Message> Handler { get; }

        protected override void InvokeHandler(Message message)
        {
            if (Control.InvokeRequired)
                Control.BeginInvoke(Handler, message);
            else
                base.InvokeHandler(message);
        }
    }
}
