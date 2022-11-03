using ACLIF.Help;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACLIF.CliVerb;

namespace ACLIF
{
    public interface ICliVerb:  IHelpItem, IHelper
    {
        string Verb { get; }
        IEnumerable<ICliVerb> CliVerbs { get; }

        bool HandlesCommand(string[] args);
        ICliVerbResult ExecuteWhenHandles(string[] args);

        string[] ProcessCommandArguments(string[] args);

        IEnumerable<SwitchProperty> Switches { get; }

        IEnumerable<OptionProperty> Options { get; }

        IEnumerable<ArgumentProperty> Arguments { get; }

    }
}
