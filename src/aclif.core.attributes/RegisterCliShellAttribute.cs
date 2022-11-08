using aclif.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.core.attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class RegisterCliShellAttribute : Attribute
    {
        public RegisterCliShellAttribute (Type cliShellType)
        {
            CliShellType = cliShellType;
        }

        public Type CliShellType { get; set; }

    }
}
