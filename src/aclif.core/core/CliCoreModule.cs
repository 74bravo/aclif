using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aclif.core
{
    public abstract class CliCoreModule : CliCoreVerb, ICliModule<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>
    {

        public abstract string Module { get; }

        #region Hide NotApplicable Verb Overrides

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override sealed string Verb => Module;

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void VerbPreExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void VerbPostExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override IEnumerable<ICliVerb> GetCoreVerbs()
        {
            return GetCoreModuleVerbs();
        }

        //protected sealed 

        #endregion

        protected virtual IEnumerable<ICliVerb> GetCoreModuleVerbs()
        {
            yield break;
        }


        internal override void PreExecute(string[] args) => ModulePreExecute(args);

        protected virtual void ModulePreExecute(string[] args) { }

        internal override void PostExecute(string[] args) => ModulePostExecute(args);

        protected virtual void ModulePostExecute(string[] args) { }

        protected override ICliVerbResult Execute(string[] args)
        {
            return VerbResult.NoAction();
        }


    }
}
