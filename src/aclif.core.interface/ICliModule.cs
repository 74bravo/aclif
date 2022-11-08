using aclif.help.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public interface ICliModule : ICliVerb
    {

    }

    public interface ICliModule<SwitchPropType, OptionPropType, ArgumentPropType> : ICliModule,  ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType>
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
    {
    }
}
