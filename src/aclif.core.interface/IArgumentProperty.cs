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
    public interface IArgumentProperty <ArgPropType> : IArgProperty<ArgPropType>
        where ArgPropType : IHelpItem, IHelper
    {

    }
}
