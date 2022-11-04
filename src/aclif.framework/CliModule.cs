﻿using aclif.Attributes;
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
    public abstract class CliModule : CliVerb, ICliModule
    {

        public virtual string Module => 
            !ModuleAttribute.IsEmpty 
            ? ModuleAttribute.Module 
            : throw new NotImplementedException("Module property must be implemented or use CliModule Attribute");

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override sealed string Verb => Module;

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

        private CliModuleAttribute? _moduleAttribute;
        protected CliModuleAttribute ModuleAttribute => 
            _moduleAttribute ??= GetType().GetCustomAttribute<CliModuleAttribute>() ?? CliModuleAttribute.Empty;

        internal override void PreExecute(string[] args) => ModulePreExecute(args);

        protected virtual void ModulePreExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void VerbPreExecute(string[] args) { }

        internal override void PostExecute(string[] args) => ModulePostExecute(args);

        protected virtual void ModulePostExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void VerbPostExecute(string[] args) { }

        protected override ICliVerbResult Execute(string[] args)
        {
            return VerbResult.NoAction();
        }

        //public override ICliVerbResult ExecuteWhenHandles(string[] args)
        //{
        //    //TODO  : Implement MultiThreaded Processing.


        //    ICliVerbResult? result = null;

        //    var nextVerbArgs = ProcessCommandArguments(args);

        //    PreExecute(args);

        //    foreach (ICliVerb verb in CliVerbs)
        //    {
        //        if (verb.HandlesCommand(nextVerbArgs))
        //        {
        //            result = verb.ExecuteWhenHandles(nextVerbArgs);
        //            //if (result.CommandHandled) return result;
        //            break;
        //        }
        //    }

        //    result = result ?? Execute(nextVerbArgs);

        //    PostExecute(args);

        //    return result;

        //}
    }
}