using aclif.help.Interface;
using aclif.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public interface ISwitchProperty<SwitchPropType> : IArgProperty<SwitchPropType>
        where SwitchPropType : IHelpItem, IHelper
    {
    }
}
