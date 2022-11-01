using ACLIF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
    public abstract class CliVerb : ICliVerb
    {

        public CliVerb ()
        {
            Console.WriteLine($"Instantiating {this.GetType().FullName}");
        }
        public virtual string verb => VerbAttributes.Verb;
        public virtual string Description => VerbAttributes.Description;
        public virtual string Help => VerbAttributes.HelpText;

        public string[] Arguments { get; protected set; }

        public virtual ICliVerbResult ExecuteWhenHandles(string[] args)
        {
            var nextVerbArgs = ProcessCommandOptions(args);

            return Execute(nextVerbArgs);
        }
        public virtual bool HandlesCommand(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                return this.verb == args[0].Trim(' ').ToLower();
            }
            return false;
        }

        private CliVerbAttribute _verbAttributes;
        protected CliVerbAttribute VerbAttributes
        {
            get
            {
                if (_verbAttributes == null)
                {
                    _verbAttributes = GetType().GetCustomAttribute<CliVerbAttribute>();
                }
                return _verbAttributes;
            }
        }

        public virtual string[] ProcessCommandOptions(string[] args)
        {
            var Arguments = args;
            // Skip the arg for the current verb if it's not the root verb.
            if (!string.IsNullOrEmpty(this.verb))
            {
                Arguments = Arguments.Skip(1).ToArray();
            }

            string arg;
            int i = 0;
            for (i = 0; i < Arguments.Length; i++)
            {
                arg = Arguments[i].Trim(' ').ToLower();

                switch (arg)
                {
                    case "-h":
                    case "--help":
                    case "-?":
                    case "/?":
                        return new string[] { "--help" };
                }

                if (arg.StartsWith("--"))
                {
                    ProcessOption(arg, Arguments, ref i);
                }
                else if (arg.StartsWith("-"))
                {
                    ProcessOptionShortCut(arg, Arguments, ref i);
                }
                else
                {
                    // All Options have been processed
                    break;
                }

            }

            Arguments = Arguments.Skip(i).ToArray();

            return Arguments;
        }

        protected void ProcessOption(string arg, string[] args, ref int index)
        {
            Console.WriteLine($"Processing Arg {arg}");
        }

        protected void ProcessOptionShortCut(string arg, string[] args, ref int index)
        {
            Console.WriteLine($"Processing Arg Shortcut {arg}");
        }

        protected abstract ICliVerbResult Execute(string[] args);

    }
}
