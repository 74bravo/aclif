using aclif;
using aclif.Attributes;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace _74Bravo.aclif.Debug.Command2
{
    [Export(typeof(ICliModule))]
    [CliModule("module2", Description = "aclif debug test Module 2")]
    public class Module2 : CliModule
    {
        //public override string Module => "module2";

        //public override string Description => "aclif debug test Module 2";

        //public override string Help => "aclif debug test Module 2 HELP";

        protected override IEnumerable<ICliVerb> GetModuleVerbs()
        {
            yield return new verb2();
        }

        protected override ICliVerbResult Execute(string[] args)
        {
            Log.Debug("Executing Module 2");

            return base.Execute(args);
        }
    }
}