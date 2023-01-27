using Toolbox.Messaging.Forms;

namespace Toolbox.Messaging.Client
{
    class ClientReceiver : ControlReceiver
    {
        public ClientReceiver(ClientForm form, string connection) : base(form, connection)
        {
            Form = form;
        }

        public ClientForm Form { get; private set; }

#pragma warning disable IDE0051         // MessageHandler are called implicitly
        [MessageHandler("answer")]
        private void Answer(string name)
        {
            Form.GotAnswer(name);
        }

        [MessageHandler("datareply")]
        private void GotDataReply(MessageData data)
        {
            Form.GotDataReply(data);
        }
#pragma warning restore IDE0051
    }
}
