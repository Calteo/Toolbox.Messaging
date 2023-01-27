using System;

namespace Toolbox.Messaging.Schemes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class SchemeAttribute : Attribute
    {
        public SchemeAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
