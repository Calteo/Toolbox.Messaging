using System;

namespace Toolbox.Messaging
{
    [Serializable]
    public class Message
    {
        public Message(string name, params object[] args)
        {
            Name = name;
            Arguments = args;
        }

        public string Name { get; }
        public object[] Arguments { get; }
    }
}
