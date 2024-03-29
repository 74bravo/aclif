﻿using static aclif.Caching;
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

        public static Session? Instance { get; private set; }

        private static string DetermineSessionId()
        {
            //ToDo  Create Session Caching Attributes to store values.
            return DefaultSessionId;
        }

        public void StartOrOpen(string sessionId, int ttl = 360)
        {
        }

        public void StartOrOpen(int ttl = 360) => StartOrOpen(DefaultSessionId, ttl);

        public void Save()
        {
        }

        public void Close()
        {
        }

    }


}
