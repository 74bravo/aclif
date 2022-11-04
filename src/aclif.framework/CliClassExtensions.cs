﻿using aclif.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static class CliClassExtensions
    {

        internal static IEnumerable<ICliVerb> SetParents (this IEnumerable<ICliVerb>  cliVerbs, IHelpItem parent)
        {
            foreach (ICliVerb child in cliVerbs)
            {
                child.ParentHelpItem = parent;
                yield return child;
            }
        }
    }
}
