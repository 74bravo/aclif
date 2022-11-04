using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Help
{
    public sealed class EmptyHelpItem : IHelpItem
    {
        public string HelpFormat => String.Empty;

        public string HelpLabel => String.Empty;

        public string Description => String.Empty;

        public string[] HelpArguments => new string[] { } ;

        public bool Hidden => false;

        public IHelpItem ParentHelpItem => throw new NotImplementedException();
    }
}
