using aclif.help.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public interface ICliRoot : ICliModule
    {


    }
    public interface ICliRoot <SwitchPropType, OptionPropType, ArgumentPropType> : ICliRoot, ICliModule<SwitchPropType, OptionPropType, ArgumentPropType>
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
    {


    }

}
