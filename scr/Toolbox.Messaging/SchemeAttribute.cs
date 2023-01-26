using System;

namespace Toolbox.Messaging
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class SchemeAttribute : Attribute
    {
        public SchemeAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
