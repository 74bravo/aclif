using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
    internal class HelpVerb : CliVerb
    {

        private string _description;
        private string _help;

        public HelpVerb(string description, string help)
        {
            _description = description;
            _help = help;
        }

        public override string Verb => "--help";

        public override string Description => _description;

        public override string Help => _help;

        protected override ICliVerbResult Execute(string[] args)
        {
            Console.WriteLine(_description);
            Console.WriteLine(_help);

            return new VerbResult(true, "", 0);

        }
    }
}
