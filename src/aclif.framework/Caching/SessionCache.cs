using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static partial class Caching
    { 

    public class SessionCache : ISerializable, IDisposable
    {
        public const string DefaultSessionId = "ACLIF-Default-Session";
        

        internal Dictionary<string,object> SessionProperties { get; set; }

        public SessionCache (string sessionId)
        {
            SessionId = sessionId;
            SessionProperties = new Dictionary<string, object>();
        }

        public SessionCache() : this(DefaultSessionId) { }

        public string SessionId { get; private set; }

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
}
