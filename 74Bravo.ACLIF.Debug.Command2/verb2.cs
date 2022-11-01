using ACLIF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _74Bravo.ACLIF.Debug.Command2
{
    public class verb2 : CliVerb
    {
        public override string verb => "verb2";

        public override string Description => "Verb2 Description";

        public override string Help => "Verb2 Help";

        protected override ICliVerbResult Execute(string[] args)
        {
            foreach (string arg in args)
            {
                Console.WriteLine($"Verb 2 Arg: {arg}");
            }
            Console.WriteLine("Running Verb 2");
            return VerbResult.Success();
        }
    }
}
