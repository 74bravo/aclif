using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aclif.help;
using aclif.help.Interface;

namespace aclif
{
    public abstract class CliSimpleVerb : ICliVerb, IHelper
    {
        private bool disposedValue;

        public abstract string Verb { get; }
        public abstract string Description { get; }
        // public abstract string Help { get; }

        public virtual bool HandlesCommand(string[] args)
        {
            if (args.Length == 0) return false;
            return Verb == args[0].Trim(' ').ToLower();
        }

        protected abstract ICliVerbResult Execute(string[] args);

        public virtual void Help(int depth)
        {
            this.LogHelp(depth);
        }

        public ICliVerbResult ExecuteWhenHandles(string[] args)
        {
            if (!IsReadyToExecute (args, out string notReadyMessage))
            {
                if (string.IsNullOrEmpty(notReadyMessage)) return VerbResult.NoAction();
                return VerbResult.NoAction(notReadyMessage);
            }
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

        //IEnumerable<ISwitchProperty> ICliVerb.Switches { get { yield break; } }

        //IEnumerable<IOptionProperty> ICliVerb.Options { get { yield break; } }

        //IEnumerable<IArgumentProperty> ICliVerb.Arguments { get { yield break; } }

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
        // ~CliSimpleVerb()
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

        public virtual bool IsReadyToExecute(string[] args, out string notReadyMessage)
        {
            notReadyMessage = string.Empty;
            return true;
        } 
    }
}
