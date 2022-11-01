using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
    public abstract class CLIModule : CliVerb, ICliModule
    {

        private IEnumerable<ICliVerb> _cliVerbs;

        public IEnumerable<ICliVerb> CliVerbs
        {
            get
            {
                if (_cliVerbs == null)
                {
                    _cliVerbs = GetHelpVerb();
                    var _moduleVerbs = GetVerbs();
                    if (_moduleVerbs != null)
                    {
                        _cliVerbs = _cliVerbs.Concat(_moduleVerbs);
                    }
                    //_cliVerbs.Concat(_otherVerbs);
                }
                return _cliVerbs;
            }
        }

        private IEnumerable<ICliVerb> GetHelpVerb ()
        {
            yield return new HelpVerb(Description, Help);
        }

        public abstract string Module { get; }

        public override sealed string verb => Module;

        protected abstract IEnumerable<ICliVerb> GetVerbs();
        //{

        //    yield break;
        //}

        protected override ICliVerbResult Execute(string[] args)
        {
            throw new NotImplementedException();
        }

        public override ICliVerbResult ExecuteWhenHandles(string[] args)
        {
            //TODO  : Implement MultiThreaded Processing.

            var nextVerbArgs = ProcessCommandOptions(args);

            foreach (ICliVerb verb in CliVerbs)
            {
                if (verb.HandlesCommand(nextVerbArgs))
                {
                    var result = verb.ExecuteWhenHandles(nextVerbArgs);
                    //if (result.CommandHandled) return result;
                    return result;
                }
            }

            return Execute(nextVerbArgs);

        }
    }
}
