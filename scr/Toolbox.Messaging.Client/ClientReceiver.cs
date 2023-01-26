using Toolbox.Messaging.Forms;

namespace Toolbox.Messaging.Client
{
    class ClientReceiver : ControlReceiver
    {
        public ClientReceiver(ClientForm form) : base(form)
        {
            Form = form;
        }

        public ClientForm Form { get; private set; }

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
    }
}
