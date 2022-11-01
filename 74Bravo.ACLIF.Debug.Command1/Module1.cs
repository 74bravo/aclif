using ACLIF;
using System.ComponentModel.Composition;

namespace _74Bravo.ACLIF.Debug.Command1
{

    [Export(typeof(ICliModule))]
    public class Module1 : CLIModule
    {
        public override string Module => "module1";

        public override string Description => "ACLIF debug test Module1";

        public override string Help => "ACLIF debug test Module 1 HELP";

        protected override IEnumerable<ICliVerb> GetVerbs()
        {
            yield break;
        }
    }
}