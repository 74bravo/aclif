using aclif.help.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public interface ICliVerb:  IHelpItem, IHelper, IDisposable
    {
        string Verb { get; }
        IEnumerable<ICliVerb> CliVerbs { get; }

        bool HandlesCommand(string[] args);

        bool IsReadyToExecute(string[] args, out string notReadyMesage);

        ICliVerbResult ExecuteWhenHandles(string[] args);

        string[] ProcessCommandArguments(string[] args);

    }

    public interface ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> : ICliVerb, IHelpItem, IHelper, IDisposable
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
    {

        IEnumerable<ISwitchProperty<SwitchPropType>> Switches { get; }

        IEnumerable<IOptionProperty<OptionPropType>> Options { get; }

        IEnumerable<IArgumentProperty<ArgumentPropType>> Arguments { get; }

    }

}
