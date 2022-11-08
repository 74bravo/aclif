using aclif.Attributes;
using aclif.core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public abstract class CliModule : CliCoreModule
    {

        public override string Module => 
            !ModuleAttribute.IsEmpty 
            ? ModuleAttribute.Module 
            : throw new NotImplementedException("Module property must be implemented or use CliModule Attribute");


        public override string Description => 
            !ModuleAttribute.IsEmpty 
            ? ModuleAttribute.Description 
            : throw new NotImplementedException("Description property must be implemented or use CliModule Attribute");

        public override string HelpFormat => 
            !ModuleAttribute.IsEmpty 
            ? ModuleAttribute.HelpFormat 
            : throw new NotImplementedException("Help property must be implemented or use CliModule Attribute");

        public override string HelpLabel =>
             !ModuleAttribute.IsEmpty
            ? String.IsNullOrEmpty(ModuleAttribute.HelpLabel)
            ? ModuleAttribute.Module
            : ModuleAttribute.HelpLabel
            : Module;


        protected sealed override IEnumerable<ICliVerb> GetCoreModuleVerbs()
        {
            return base.GetCoreModuleVerbs().Concat(GetModuleVerbs());
        }

        protected virtual IEnumerable<ICliVerb> GetModuleVerbs()
        {
            yield break;
        }


        private CliModuleAttribute? _moduleAttribute;
        protected CliModuleAttribute ModuleAttribute => 
            _moduleAttribute ??= GetType().GetCustomAttribute<CliModuleAttribute>() ?? CliModuleAttribute.Empty;

    }
}
