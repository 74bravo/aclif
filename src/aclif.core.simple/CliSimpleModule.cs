using aclif.simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.core.simple
{
    public abstract class CliSimpleModule : CliSimpleVerb, ICliModule
    {

        public abstract string Module { get; }
        public sealed override string Verb => Module;




    }
}
