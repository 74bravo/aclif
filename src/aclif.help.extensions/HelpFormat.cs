using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.help
{

        public static class Formats
        {

            public const string Root = "  {0}\t\t{1}";
            public const string RootUsage = "Usage: {0} {1}";

            public const string ModuleList = "  {0}\t\t{1}";
            public const string ModuleUsage = "Usage: {0} {1} {2}";


            public const string VerbList = "  {0}\t\t{1}";
            public const string VerbUsage = "Usage: {0} {1} {2} {3}";

            //  {0} = ShortCut; {1} = Option; {2} = description; {3} = Property Name
            public const string OptionList = "  {0} {1}<{3}>\t\t{2}";

            //  {0} = ShortCut; {1} = Option; {2} = description;
            public const string SwitchList = "  {0} {1}\t\t{2}";

            public const string DescriptionFormat = "\nDescription:\n  {0}";

        }
}
