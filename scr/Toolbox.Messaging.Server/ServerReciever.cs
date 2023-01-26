using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Messaging.Forms;

namespace Toolbox.Messaging.Server
{
    class ServerReceiver : ControlReceiver
    {
        public ServerReceiver(ServerForm form) : base(form)
        {
            Form = form;
        }

        public ServerForm Form { get; private set; }

        [MessageHandler("hello")]
        private void Hello(string name)
        {
            Form.AddHello(name);
        }

        [MessageHandler("sayhello")]
        private void SayHello(string name, Sender replyTo)
        {
            Form.SayHello(name);          

            replyTo.Post("answer", $"Hello {name}.");
        }

        [MessageHandler("data")]
        private void GotData(MessageData data, Sender replyTo)
        {
            Form.GotData(data);

            replyTo.Post("datareply", data);
        }

    }
}
