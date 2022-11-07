using aclif.Attributes;
using aclif.help;
using aclif.help.Interface;
using aclif.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public abstract class CliCoreVerb : ICliVerb<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>
    {
        //public IEnumerable<ISwitchProperty<CliVerbSwitchAttribute>> Switches => SwitchDictionary.Values;
        //public IEnumerable<IOptionProperty<CliVerbOptionAttribute>> Options => OptionDictionary.Values;
        //public IEnumerable<IArgumentProperty<CliVerbArgumentAttribute>> Arguments => ArgDictionary;

        IEnumerable<ISwitchProperty<CliVerbSwitchAttribute>> ICliVerb<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>.Switches => SwitchDictionary.Values;
        IEnumerable<IOptionProperty<CliVerbOptionAttribute>> ICliVerb<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>.Options => OptionDictionary.Values;
        IEnumerable<IArgumentProperty<CliVerbArgumentAttribute>> ICliVerb<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>.Arguments => ArgDictionary;





        private CliVerbAttribute? _verbAttribute;
        protected CliVerbAttribute VerbAttribute =>
            _verbAttribute ??= GetType().GetCustomAttribute<CliVerbAttribute>() ?? CliVerbAttribute.Empty;


        public class ArgumentProperty : ArgProperty<CliVerbArgumentAttribute>, IArgumentProperty<CliVerbArgumentAttribute>
        { internal ArgumentProperty(PropertyInfo pi, CliVerbArgumentAttribute arg) : base(pi, arg) { } }

        public class OptionProperty : ArgProperty<CliVerbOptionAttribute>, IOptionProperty<CliVerbOptionAttribute>
        { internal OptionProperty(PropertyInfo pi, CliVerbOptionAttribute arg) : base(pi, arg) { } }

        public class SwitchProperty : ArgProperty<CliVerbSwitchAttribute>, ISwitchProperty<CliVerbSwitchAttribute>
        { internal SwitchProperty(PropertyInfo pi, CliVerbSwitchAttribute arg) : base(pi, arg) { } }

        public abstract class ArgProperty<T> : IArgProperty<T>
            where T : CliVerbArgumentAttribute
        {
            public PropertyInfo pi { get; private set; }
            public T arg { get; private set; }

            internal ArgProperty(PropertyInfo pi, T arg)
            {
                this.pi = pi;
                this.arg = arg;
            }

        }

        private bool _argMembersLoaded = false;
        private void LoadArgMembers()
        {
            if (!_argMembersLoaded)
            {
                foreach (MemberInfo property in this.GetType().GetProperties())
                {
                    var vArg = property.GetCustomAttribute<CliVerbArgumentAttribute>() ?? CliVerbArgumentAttribute.Empty;

                    //Setting ParentHelpItem
                    vArg.ParentHelpItem = this;

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


        private Dictionary<string, OptionProperty>? _OptionDictionary;
        private Dictionary<string, OptionProperty> OptionDictionary => _OptionDictionary ??= new Dictionary<string, OptionProperty>();

        private Dictionary<string, SwitchProperty>? _SwitchDictionary;
        private Dictionary<string, SwitchProperty> SwitchDictionary => _SwitchDictionary ??= new Dictionary<string, SwitchProperty>();

        private List<ArgumentProperty>? _ArgDictionary;
        private List<ArgumentProperty> ArgDictionary => _ArgDictionary ??= new List<ArgumentProperty>();

        public string[]? Arguments { get; protected set; }
        public string[] ProcessCommandArguments(string[] args)
        {

            Arguments = args;
            // Skip the arg for the current verb if it's not the root verb.
            //TODO check to see if root...
            if (!(this is ICliRoot))
            //if (!string.IsNullOrEmpty(this.Verb) )
            {
                Arguments = Arguments.Length > 1 ? Arguments.Skip(1).ToArray() : new String[] { };
            }

            string arg;
            int i = 0;
            int Lasti = i;
            bool handled = true;
            for (i = 0; i < Arguments.Length; i++)
            {
                arg = Arguments[i].Trim(' ').ToLower();

                //switch (arg)
                //{
                //    case "-h":
                //    case "--help":
                //    case "-?":
                //    case "/?":
                //        return new string[] { "--help" };
                //}

                LoadArgMembers();

                if (arg.StartsWith("--"))
                {
                    handled = ProcessOptionLongName(arg, Arguments, ref i);
                }
                else if (arg.StartsWith("-"))
                {
                    handled = ProcessOptionShortCut(arg, Arguments, ref i);
                }
                else
                {
                    handled = ProcessArgument(arg, Arguments, ref i);
                }

                if (!handled) break;

                Lasti = i;
            }

            if (handled)
            {
                Arguments = i > 1 ? Arguments.Skip(Lasti).ToArray() : new String[] { };
            }
            else
            {
                Arguments = i > 0 ? Arguments.Skip(i).ToArray() : Arguments;
            }




            return Arguments;
        }

        private int _nextArgIndex = 0;
        private bool disposedValue;

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
            Log.Trace($"Processing Arg {arg}");
            return ProcessOption(arg, args, ref index);
        }

        private bool ProcessOptionShortCut(string arg, string[] args, ref int index)
        {
            // Use this if additional processing is required for shortcuts.
            Log.Trace($"Processing Arg Shortcut {arg}");
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

        public virtual ICliVerbResult ExecuteWhenHandles(string[] args)
        {
            //TODO  : Implement MultiThreaded Processing.

            ICliVerbResult? result = null;

            var nextVerbArgs = ProcessCommandArguments(args);

            PreExecute(nextVerbArgs);

            if (nextVerbArgs.Length > 0)
            {

                //TODO:   Implement Async Here....

                foreach (ICliVerb verb in CliVerbs)
                {

                    if (verb.HandlesCommand(nextVerbArgs))
                    {
                        result = verb.ExecuteWhenHandles(nextVerbArgs);
                        //if (result.CommandHandled) return result;
                        break;
                    }
                }
                result = result ?? VerbResult.Exception(new ArgumentException("Unknown Arguments Provided"));
            }
            else
            {
                //Running Shell

                if (!IsReadyToExecute(args, out string notReadyMessage))
                {
                    if (string.IsNullOrEmpty(notReadyMessage)) return VerbResult.NoAction();
                    return VerbResult.NoAction(notReadyMessage);
                }

                result = result ?? Execute(nextVerbArgs);

            }

            PostExecute(nextVerbArgs);

            return result;

        }

        internal virtual void PreExecute(string[] args) => VerbPreExecute(args);

        protected virtual void VerbPreExecute(string[] args) { }

        protected abstract ICliVerbResult Execute(string[] args);

        internal virtual void PostExecute(string[] args) => VerbPostExecute(args);

        protected virtual void VerbPostExecute(string[] args) { }


        public virtual bool HandlesCommand(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                return this.Verb == args[0].Trim(' ').ToLower();
            }
            return false;
        }

        #region Abstract Methods

        public abstract string Verb { get; }
        public abstract IEnumerable<ICliVerb> CliVerbs { get; }
        public abstract string HelpFormat { get; }
        public abstract string HelpLabel { get; }
        public abstract string Description { get; }



        #endregion
        public virtual string[] HelpArguments => new[] { Verb, Description };

        public virtual bool Hidden => false;

        public IHelpItem? ParentHelpItem { get; set; }



        // public abstract void Dispose();
        //public abstract ICliVerbResult ExecuteWhenHandles(string[] args);
        //public abstract bool HandlesCommand(string[] args);

        public virtual void Help(int depth)
        {
            if (!Hidden)
            {
                LoadArgMembers();

                //Description:
                this.LogDescription();

                if (this is ICliRoot<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>)
                {

                    ((ICliRoot<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>)this)
                        .LogRootUsage()
                        .LogRootOptions()
                        .LogRootArguments()
                        .LogRootMethods();
                }
                else if (this is ICliModule<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>)
                {
                    ((ICliModule<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>)this)
                        .LogModuleUsage()
                        .LogModuleOptions()
                        .LogModuleArguments()
                        .LogModuleVerbs();
                }
                else
                {
                    ((ICliVerb<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>)this)
                        .LogVerbUsage()
                        .LogVerbOptions()
                        .LogVerbArguments()
                        .LogVerbSubVerbs();
                }
            }
        }
        public virtual bool IsReadyToExecute(string[] args, out string notReadyMesage)
        {
            notReadyMesage = string.Empty;
            //TODO:  Implement Smart Argument Validation when Ready
            return true;
        }

        //  public abstract string[] ProcessCommandArguments(string[] args);


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CliVerb()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
