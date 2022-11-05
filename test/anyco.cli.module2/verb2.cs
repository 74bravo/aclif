using aclif;
using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _74Bravo.aclif.Debug.Command2
{

    public enum Opt3Type
    {
        eat,
        shorts,
        stay
    }


    [CliVerb("verb2",Description = "Verb2 Description")]
    public class verb2 : CliVerb
    {
        //public override string verb => "verb2";

        //public override string Description => "Verb2 Description";

        //public override string Help => "Verb2 Help";

        [CliVerbOption(longName: "--opt1")]
        public string opt1 { get; set; } = string.Empty;


        [CliVerbOption(longName: "--opt2")]
        public int opt2 { get; set; }

        [CliVerbOption(longName: "--opt3")]
        public Opt3Type opt3 { get; set; }

        [CliVerbOption(longName: "--opt4")]
        public DateOnly opt4 { get; set; }

        [CliVerbOption(longName: "--opt5")]
        public DirectoryInfo? opt5 { get; set; }

        [CliVerbArgument]
        public DirectoryInfo? Arg1 { get; set; }

        [CliVerbArgument]
        public string? Arg2 { get; set; }

        public Session<int> TestInt { get; set; }

        //[CliVerbArgument]
        //public string Arg2 { get; set; }

        protected override ICliVerbResult Execute(string[] args)
        {
            //foreach (string arg in args)
            //{
            //    Console.WriteLine($"Verb 2 Arg: {arg}");
            //}

            TestInt = 5;

            var temp = TestInt;

            int temp2 = TestInt;

           Console.WriteLine("Running Verb 2");
            return VerbResult.Success();
        }
    }
}
