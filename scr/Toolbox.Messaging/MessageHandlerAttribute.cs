using System;

namespace Toolbox.Messaging
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MessageHandlerAttribute : Attribute
    {
        public MessageHandlerAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
