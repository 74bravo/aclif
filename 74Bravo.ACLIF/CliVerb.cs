using ACLIF.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace ACLIF
{
    public abstract class CliVerb : ICliVerb
    {
        public CliVerb()
        {
           // Console.WriteLine($"Instantiating {this.GetType().FullName}");
        }
        public virtual string Verb => 
            !VerbAttribute.IsEmpty 
            ? VerbAttribute.Verb 
            : throw new NotImplementedException("Verb property must be implemented or use CliVerb Attribute");
        
        public virtual string Description => 
            !VerbAttribute.IsEmpty 
            ? VerbAttribute.Description 
            : throw new NotImplementedException("Description property must be implemented or use CliVerb Attribute");

        public virtual string Help => 
            !VerbAttribute.IsEmpty 
            ? VerbAttribute.HelpText 
            : throw new NotImplementedException("Help property must be implemented or use CliVerb Attribute");

        public string[] Arguments { get; protected set; }

        public virtual ICliVerbResult ExecuteWhenHandles(string[] args)
        {
            var nextVerbArgs = ProcessCommandArguments(args);

            PreExecute(args);

            var result = Execute(nextVerbArgs);

            PostExecute(args);

            return result;
        }

        internal virtual void PreExecute(string[] args) => VerbPreExecute(args); 

        protected virtual void VerbPreExecute(string[] args) {}

        internal virtual void PostExecute(string[] args) => VerbPostExecute(args);

        protected virtual void VerbPostExecute(string[] args) {}

        public virtual bool HandlesCommand(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                return this.Verb == args[0].Trim(' ').ToLower();
            }
            return false;
        }

        private CliVerbAttribute? _verbAttribute;
        protected CliVerbAttribute VerbAttribute
        {
            get
            {
                return _verbAttribute ??= GetType().GetCustomAttribute<CliVerbAttribute>() ?? CliVerbAttribute.Empty;
            }
        }

        //private IEnumerable<ArgMembers>? _argMembers;
        //private IEnumerable<ArgMembers> argMembers
        //{ get { return _argMembers ??= getArgMembers(); } }


        //private class ArgProperty
        //{
        //    public PropertyInfo pi;
        //    public CliVerbArgumentAttribute arg;

        //    public ArgProperty(PropertyInfo pi, CliVerbArgumentAttribute arg)
        //    {
        //        this.pi = pi;
        //        this.arg = arg;
        //    }

        //}

        private class ArgumentProperty : ArgProperty<CliVerbArgumentAttribute> 
            { internal ArgumentProperty(PropertyInfo pi, CliVerbArgumentAttribute arg) : base(pi, arg) { } }

        private class OptionProperty : ArgProperty<CliVerbOptionAttribute>
        { internal OptionProperty(PropertyInfo pi, CliVerbOptionAttribute arg) : base(pi, arg) { } }

        private class SwitchProperty : ArgProperty<CliVerbSwitchAttribute>
        { internal SwitchProperty(PropertyInfo pi, CliVerbSwitchAttribute arg) : base(pi, arg) { } }

        private abstract class ArgProperty <T> where T : CliVerbArgumentAttribute
        {
            public PropertyInfo pi;
            public T arg;

            internal ArgProperty(PropertyInfo pi, T arg)
            {
                this.pi = pi;
                this.arg = arg;
            }

        }

        private Dictionary<string, OptionProperty>? _OptionDictionary;
        private Dictionary<string, OptionProperty> OptionDictionary => _OptionDictionary ??= new Dictionary<string, OptionProperty>();

        private Dictionary<string, SwitchProperty>? _SwitchDictionary;
        private Dictionary<string, SwitchProperty> SwitchDictionary => _SwitchDictionary ??= new Dictionary<string, SwitchProperty>();

        private List<ArgumentProperty>? _ArgDictionary;
        private List<ArgumentProperty> ArgDictionary => _ArgDictionary ??= new List<ArgumentProperty>();


        private bool _argMembersLoaded = false;
        private void LoadArgMembers()
        {
            if (!_argMembersLoaded)
            {
                foreach (MemberInfo property in this.GetType().GetProperties())
                {
                    var vArg = property.GetCustomAttribute<CliVerbArgumentAttribute>() ?? CliVerbArgumentAttribute.Empty;

                    if (!vArg.IsEmpty)
                    {
                        if (vArg is CliVerbOptionAttribute)
                        {
                            var optMbr = new OptionProperty((PropertyInfo)property, (CliVerbOptionAttribute)vArg);
                            if (!String.IsNullOrEmpty(optMbr.arg.LongName)) OptionDictionary.Add(optMbr.arg.LongName, optMbr);
                            if (!String.IsNullOrEmpty(optMbr.arg.ShortName)) OptionDictionary.Add(optMbr.arg.ShortName, optMbr);
                        }
                        else if (vArg is CliVerbSwitchAttribute)
                        {
                            var optMbr = new SwitchProperty((PropertyInfo)property, (CliVerbSwitchAttribute)vArg);
                            if (!String.IsNullOrEmpty(optMbr.arg.LongName)) SwitchDictionary.Add(optMbr.arg.LongName, optMbr);
                            if (!String.IsNullOrEmpty(optMbr.arg.ShortName)) SwitchDictionary.Add(optMbr.arg.ShortName, optMbr);
                        }
                        else
                        {
                            ArgDictionary.Add(new ArgumentProperty((PropertyInfo)property, vArg));
                        }
                    }
                 }
            }
            _argMembersLoaded = true;
        }

        public string[] ProcessCommandArguments(string[] args)
        {
            Arguments = args;
            // Skip the arg for the current verb if it's not the root verb.
            if (!string.IsNullOrEmpty(this.Verb))
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

                LoadArgMembers();

                if (arg.StartsWith("--"))
                {
                    if (!ProcessOptionLongName(arg, Arguments, ref i)) break;
                }
                else if (arg.StartsWith("-"))
                {
                    if (!ProcessOptionShortCut(arg, Arguments, ref i)) break;
                }
                else
                {
                    if (!ProcessArgument(arg, Arguments, ref i)) break;
                }
            }

            Arguments = Arguments.Skip(i).ToArray();

            return Arguments;
        }

        private int _nextArgIndex = 0;
        private bool ProcessArgument(string arg, string[] args, ref int index)
        {
            if (ArgDictionary.Count == 0) return false;

            var argProp = ArgDictionary[_nextArgIndex];
            if (argProp == null) return false;
            _nextArgIndex++;

            string txtValue = arg;
            var val = argProp.pi.PropertyType.FromString(txtValue);

            argProp.pi.SetValue(this, val);

            return true;
        }

        private bool ProcessOptionLongName(string arg, string[] args, ref int index)
        {
            // Use this if additional processing is required for longnames.
            Console.WriteLine($"Processing Arg {arg}");
            return ProcessOption(arg, args, ref index);
        }

        private bool ProcessOptionShortCut(string arg, string[] args, ref int index)
        {
            // Use this if additional processing is required for shortcuts.
            Console.WriteLine($"Processing Arg Shortcut {arg}");
            return ProcessOption(arg, args, ref index);
        }

        private bool ProcessOption(string arg, string[] args, ref int index)
        {
            string txtValue = string.Empty;
            if (OptionDictionary.ContainsKey(arg))
            {
                var optProp = OptionDictionary[arg];

                if (optProp.arg.ArgValueIsNext)
                {
                    index++;
                    txtValue = args[index];
                }
                else
                {
                    txtValue = arg.Split(optProp.arg.Separator)[1];
                }

                var val = optProp.pi.PropertyType.FromString(txtValue);

                optProp.pi.SetValue(this, val);
            }
            else if (SwitchDictionary.ContainsKey(arg))
            {
                // If the switch is found, then the associated property should be set to true.
                // Switch properties should be of boolean type.

                var switchProp = SwitchDictionary[arg];

                switchProp.pi.SetValue(this, true);

            }
            else
            {
                return false;
            }
            return true;
        }

        protected abstract ICliVerbResult Execute(string[] args);

    }
}
