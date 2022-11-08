using aclif.core.simple;
using aclif.simple;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.framework.module.shell
{
    //[Export(typeof(ICliModule))]
    public class ShellVerb : CliSimpleModule
    {
        public override string Module => "Shell";

        public override string Description => "Used to enter into a command line shell mode";

        public override bool HandlesCommand(string[] args)
        {
            // shell is started when there are no other command arguments.
            return args.Length == 0;

        }

        protected override ICliVerbResult Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
