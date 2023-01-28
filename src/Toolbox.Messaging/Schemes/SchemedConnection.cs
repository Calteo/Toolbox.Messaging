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
            // TODO: better initialization
            SchemedTypes = Assembly.GetExecutingAssembly().GetTypes()
                            .Where(t => typeof(T).IsAssignableFrom(t) && t.GetCustomAttribute<SchemeAttribute>() != null)
                            .ToDictionary(t => t.GetCustomAttribute<SchemeAttribute>()?.Name ?? throw new Exception());
        }

        private static Dictionary<string, Type> SchemedTypes { get; }

        internal static T Create(string connection, params object[]? args)
        {
            var uri = new Uri(connection);

            if (!SchemedTypes.TryGetValue(uri.Scheme, out var schemedType))
                throw new ArgumentException($"Invalid scheme on connection ({connection}).", nameof(connection));

            var parameters = new object[1 + args?.Length ?? 0];
            parameters[0] = connection;
            if (args != null)
                args.CopyTo(parameters, 1);

            var schemedConnection = Activator.CreateInstance(schemedType, parameters);
            if (schemedConnection == null)
                throw new ArgumentException($"Can't instantiate {schemedType.FullName} for connection {connection}.", nameof(connection));

            return (T)schemedConnection;
        }

        protected SchemedConnection(string connection)
        {
            Connection = connection;
        }

        public string Connection { get; protected set; }

        protected string TraceCategory => $"{GetType().Name}[{Connection}]";
    }
}
