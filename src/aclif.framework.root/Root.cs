using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aclif.Attributes;

namespace aclif.framework
{

    [CliRoot(Description = "aclif.Framework Root", HelpLabel = "74Bravo.aclif.Debug")]
    public class Root : CliRoot
    {
        public static int Go()
        {
            return Root.Invoke();
        }

        public static int Invoke()
         => Invoke<Root>();
    }
}
