using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{

    public static partial class Shell
    { 
        internal class ExitVerb : CliSimpleVerb
        {
            public override string Verb => "exit";

            public override string Description => "Use to exit shell mode";

            protected override ICliVerbResult Execute(string[] args)
            {

                return VerbResult.Success("Exiting Verb Mode");
            }
        }
    }
}
