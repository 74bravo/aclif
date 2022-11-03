using ACLIF;
using ACLIF.Attributes;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace _74Bravo.ACLIF.Debug.Command2
{
    [Export(typeof(ICliModule))]
    [CliModule("module2", Description = "ACLIF debug test Module 2")]
    public class Module2 : CliModule
    {
        //public override string Module => "module2";

        //public override string Description => "ACLIF debug test Module 2";

        //public override string Help => "ACLIF debug test Module 2 HELP";

        protected override IEnumerable<ICliVerb> GetVerbs()
        {
            yield return new verb2();
        }
    }
}