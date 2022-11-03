using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Help
{
    internal sealed class HelpVerb : CliSimpleVerb
    {

        //private string _description;
        //private string _help;
        private IHelper _helpProvider;

        public HelpVerb(IHelper helpProvider)
        {
            _helpProvider = helpProvider;
        }

        public IHelper HelpProvider => _helpProvider;

        public override string Verb => "--help";

        public override string Description => "Help Handler";

        public override string HelpFormat => "Provides help for commands";

        //public IEnumerable<ICliVerb> CliVerbs { get { yield break; } }

        //public ICliVerbResult ExecuteWhenHandles(string[] args)
        //{
        //    return Execute(args);
        //}

        public override bool HandlesCommand(string[] args)
        {
            var arg = args[0].Trim(' ').ToLower();

            switch (arg)
            {
                case "-h":
                case "--help":
                case "-?":
                case "/?":
                    return true;
                default:
                    return false;
            }
        }

        //public string[] ProcessCommandArguments(string[] args) { }
        //{
        //}

        protected override ICliVerbResult Execute(string[] args)
        {
            



            HelpProvider.Help(2);
            return new VerbResult(true, "", 0);
        }

        //public string GetHelp()
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine($"{Verb}\t\t{Description}");
        //    sb.AppendLine(Help);

        //    return sb.ToString();
        //}
    }
}
