using aclif.Caching;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aclif
{
    public class Session : SessionCache
    {

        private static object Lock = new object();

        public static Guid DefaultSessionId => Guid.Empty;

        public static Session? Instance { get; private set; }
        
        private static Guid DetermineSessionId()
        {
            //ToDo  Create Session Caching Attributes to store values.
            return DefaultSessionId;
        }

        public void StartOrOpen (Guid sessionId, int ttl = 360)
        {
        }

        public void StartOrOpen(int ttl = 360) => StartOrOpen(DefaultSessionId, ttl);

        public void Save ()
        {
        }

        public void Close()
        {
        }

   }


}
