using Toolbox.Messaging.Forms;

namespace Toolbox.Messaging.Server
{
    class ServerReceiver : ControlReceiver
    {
        public ServerReceiver(ServerForm form, params string[] connections) : base(form, connections)
        {
            Form = form;
        }

        public ServerForm Form { get; private set; }


#pragma warning disable IDE0051         // MessageHandler are called implicitly
        [MessageHandler("hello")]
        private void Hello(string name)
        {
            Form.AddHello(name);
        }

        [MessageHandler("sayhello")]
        private void SayHello(string name, Sender replyTo)
        {
            if (Form.SayHello(name))
                replyTo.Post("answer", $"Hello {name}!");
        }

        [MessageHandler("data")]
        private void GotData(MessageData data, Sender replyTo)
        {
            Form.GotData(data);

            replyTo.Post("datareply", data);
        }
#pragma warning restore IDE0051
    }
}