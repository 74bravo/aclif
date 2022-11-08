using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static class _root
    {
        internal static ICliRoot? Instance { get; private set; }


        internal static void SetGobalInstance (this ICliRoot root)
        {
            Instance = root;
        }

        public static ICliVerbResult DelegatedRootExecute (this ICliVerb root, string[] args)
        {
            return Instance.ExecuteWhenHandlesDelegate(args);
        }



    }
}
