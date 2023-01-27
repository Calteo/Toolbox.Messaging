using System;
using System.Collections.Generic;
using System.Linq;

namespace Toolbox.Messaging
{
    [Serializable]
    public class Message
    {
        public Message(string name, params object[] args)
        {
            Name = name;
            Arguments = args.ToList();
        }

        public string Name { get; }
        public List<object> Arguments { get; }
    }
}
