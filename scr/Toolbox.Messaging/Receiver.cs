using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Toolbox.Messaging
{
    public class Receiver : ReceiverBase
    {
        public Receiver()
        {
            var type = GetType();

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                    .Where(p => p.ReturnType == typeof(void)))
            {
                var attribute = method.GetCustomAttribute<MessageHandlerAttribute>();
                if (attribute != null)
                {
                    if (Handlers.ContainsKey(attribute.Name))
                        throw new ArgumentException($"Message '{attribute.Name}' already registered.");

                    var handler = method.CreateDelegate(
                                    Expression.GetDelegateType(method.GetParameters()
                                    .Select(p => p.ParameterType).Concat(new[] { method.ReturnType }).ToArray()
                                    ), this);

                    Handlers.Add(attribute.Name, handler);
                }
            }
        }
    }
}
