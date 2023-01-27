using System.Diagnostics;
using System.IO;
using Toolbox.Messaging.Schemes;

namespace Toolbox.Messaging.Speakers
{
    /// <summary>
    /// Base class for all <see cref="Speaker"/>s.
    /// </summary>
    public abstract class Speaker : SchemedConnection<Speaker>
    {
        protected Speaker(string connection) : base(connection)
        {
        }

        internal void Post(MemoryStream stream)
        {            
            var encoded = Encode(stream);

            Trace.WriteLine($"post - {encoded.Length} bytes", TraceCategory);

            encoded.Position = 0;
            Send(encoded);

            Trace.WriteLine($"posted - {encoded.Length} bytes", TraceCategory);
        }

        protected virtual MemoryStream Encode(MemoryStream stream)
        {
            return stream;
        }

        abstract protected void Send(MemoryStream stream);
    }
}
