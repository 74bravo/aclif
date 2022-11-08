using aclif.simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    internal class CommentVerb : CliSimpleVerb
    {
        public override string Verb => "#";

        public override bool Hidden => true;

        public override string Description => "Handles Comments beginning with #";

        public override bool HandlesCommand(string[] args)
        {
            var arg1 = args.CleanFirstArgFirstChars();
            var cond = args.FirstArgStartsWith('#');
            return args.FirstArgStartsWith("#");
        }

        protected override ICliVerbResult Execute(string[] args)
        { 
            //Take no action for comments
            return VerbResult.Success(string.Empty);
        }
    }
}
