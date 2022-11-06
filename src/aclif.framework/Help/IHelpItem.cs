using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{ 
    public static partial class Help
    {
        public interface IHelpItem
        {

            string HelpFormat { get; }
            string HelpLabel { get; }
            string Description { get; }
            string[] HelpArguments { get; }
            bool Hidden { get; }
            //  IEnumerable<IHelper> HelpLoggers { get; }
            IHelpItem? ParentHelpItem { get; set; }

        }
    }
}
