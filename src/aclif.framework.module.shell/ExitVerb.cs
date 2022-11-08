using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aclif.core.simple;
using aclif.simple;

namespace aclif
{


        [Export(typeof(ICliModule))]
        internal class ExitVerb : CliSimpleModule
        {
            public override string Module => "exit";

            public override string Description => "Use to exit shell mode";

            protected override ICliVerbResult Execute(string[] args)
            {

                return VerbResult.Success("Exiting Verb Mode");
            }
        }
 
}
