using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Toolbox.Messaging.Schemes
{
    public abstract class SchemedConnection<T>
    {
        static SchemedConnection()
        {
            SchemedTypes = Assembly.GetExecutingAssembly().GetTypes()
                            .Where(t => typeof(T).IsAssignableFrom(t) && t.GetCustomAttribute<SchemeAttribute>()!=null)
                            .ToDictionary(t => t.GetCustomAttribute<SchemeAttribute>().Name);
        }

        private static Dictionary<string, Type> SchemedTypes { get; }

        internal static T Create(string connection)
        {
            var uri = new Uri(connection);

            if (!SchemedTypes.TryGetValue(uri.Scheme, out Type schemedType))
                throw new ArgumentException($"Invalid scheme on connection ({connection}).", nameof(connection));

            return (T)Activator.CreateInstance(schemedType, new object[] { connection });
        }

        protected SchemedConnection(string connection)
        {
            Connection = connection;
        }

        public string Connection { get; protected set; }

        protected string TraceCategory => $"{GetType().Name}[{Connection}]";
    }
}
