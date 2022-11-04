using aclif.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aclif.CliVerb;

namespace aclif
{
    public abstract class CliSimpleVerb : ICliVerb, IHelper
    {
        public abstract string Verb { get; }
        public abstract string Description { get; }
       // public abstract string Help { get; }

        public abstract bool HandlesCommand(string[] args);

        protected abstract ICliVerbResult Execute(string[] args);

        public virtual void Help(int depth)
        {
            this.LogHelp(depth);
        }

        public ICliVerbResult ExecuteWhenHandles(string[] args)
        {
            return Execute(args);
        }

        public IEnumerable<ICliVerb> CliVerbs { get { yield break; } }

        public virtual string HelpFormat => "\t{0}\t\t{1}";

        public virtual string HelpLabel { get; set; } = string.Empty;

        public IHelpItem? ParentHelpItem { get; set; } 

        public virtual string[] HelpArguments => new[] { Verb, Description };
        public virtual bool Hidden => false;

        public IEnumerable<IHelper> HelpLoggers { get { yield break; } }

        public string[] ProcessCommandArguments(string[] args) => args;

        IEnumerable<SwitchProperty> ICliVerb.Switches { get { yield break; } }

        IEnumerable<OptionProperty> ICliVerb.Options { get { yield break; } }

        IEnumerable<ArgumentProperty> ICliVerb.Arguments { get { yield break; } }


    }
}
