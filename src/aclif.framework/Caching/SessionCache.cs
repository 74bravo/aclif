using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace aclif.Caching
{
    public class SessionCache : ISerializable, IDisposable
    {

        internal Dictionary<string,object> SessionProperties { get; set; }

        public SessionCache (Guid sessionId)
        {
            SessionId = sessionId;
        }

        public SessionCache() : this(Guid.Empty) { }

        public Guid SessionId { get; private set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
           // throw new NotImplementedException();
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }

}
