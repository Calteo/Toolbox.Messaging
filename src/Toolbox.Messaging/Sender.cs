using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Nodes;
using Toolbox.Messaging.Speakers;

namespace Toolbox.Messaging
{
    public class Sender
    {
        public Sender(string connection)
        {
            Speaker = Speaker.Create(connection);
        }
        public Speaker Speaker { get; }
       
        public void Post(string name, params object[] args)
        {
            var message = new Message(name, args);

            OnPost(message);

            var stream = new MemoryStream();

            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, message);

            stream.Position = 0;
            Speaker.Post(stream);
        }

        protected virtual void OnPost(Message message)
        {
            for (var i = 0; i < message.Arguments.Count; i++)
            {
                if (message.Arguments[i] is Receiver receiver)
                {
                    var connection = receiver.DefaultConnection;
                    if (connection == null)
                        throw new ArgumentException("Reciever has no default listener attached.", $"args[{i}]");

                    message.Arguments[i] = new EndPoint(connection);
                }
            }
        }
    }
}