using aclif.help.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aclif.Interface
{
    public interface IArgProperty <T>
        where T : IHelpItem, IHelper
    {

        PropertyInfo pi { get; }
        T arg { get; }

    }
}
