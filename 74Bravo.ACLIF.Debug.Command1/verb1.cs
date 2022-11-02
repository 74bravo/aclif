﻿using ACLIF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _74Bravo.ACLIF.Debug.Command1
{
    public class verb1 : CliVerb
    {
        public override string Verb => "verb1";

        public override string Description => "Verb1 Description";

        public override string Help => "Verb1 Help";

        protected override ICliVerbResult Execute(string[] args)
        {
            foreach (string arg in args)
            {
                Console.WriteLine($"Verb 1 Arg: {arg}");
            }
            Console.WriteLine("Running Verb 1");
            return VerbResult.Success();
        }
    }
}
