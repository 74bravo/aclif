using aclif;
using System.ComponentModel.Composition;

namespace _74Bravo.aclif.Debug.Command1
{

    [Export(typeof(ICliModule))]
    public class Module1 : CliModule
    {
        public override string Module => "module1";

        public override string Description => "aclif debug test Module1";

        public override string HelpFormat => "{0} - {0}";

        protected override IEnumerable<ICliVerb>  GetModuleVerbs()
        {
            yield return new verb1();
        }

        protected override ICliVerbResult Execute(string[] args)
        {
            Log.Debug("Executing Module 1");

            return base.Execute(args);
        }
    }
}