using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using System.Threading;
using aclif.Attributes;
using System.Reflection;
using System.Diagnostics.SymbolStore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.CommandLine.Parsing;
using System.Diagnostics;
using aclif.core;

namespace aclif // Note: actual namespace depends on the project name.
{
    public abstract class CliRoot :   CliCoreRoot //CliModule, ICliRoot, IDisposable
    {
        //private string[] _args;

        private bool _isDisposed;

        public CliRoot() : base()
        {
            Log.Trace($"Instantiating CLIRoot() from {this.GetType().FullName}");
        }

        //public static int Invoke()
        // => Invoke<CliRoot>();

        public static int Invoke<CliRootType>()
            where CliRootType : ICliRoot, new()
        => Process.GetCurrentProcess().InvokeCli<CliRootType>();

        //public static int InvokeCli(this string[] args)
        //    => args.InvokeCli<CliRoot>();

        //public static int InvokeCli(this Process process)
        //{
        //    return process.InvokeCli<CliRoot>();
        //}





        public override string Description => 
            !RootAttribute.IsEmpty
            ? RootAttribute.Description
            : throw new NotImplementedException("Description property must be implemented or use CliRoot Attribute");

        public override string HelpFormat => 
            !RootAttribute.IsEmpty
            ? RootAttribute.HelpFormat
            : throw new NotImplementedException("Help property must be implemented or use CliRoot Attribute");

        public override string HelpLabel =>
             !RootAttribute.IsEmpty
            ? String.IsNullOrEmpty(RootAttribute.HelpLabel)
            //            ? RootAttribute.Module
            // TODO:  Grab the tool command line value.
            ? string.Empty
            : RootAttribute.HelpLabel
            : string.Empty;

        private CliRootAttribute? _rootAttribute;
        protected CliRootAttribute RootAttribute =>
            _rootAttribute ??= GetType().GetCustomAttribute<CliRootAttribute>() ?? CliRootAttribute.Empty;





        internal virtual IEnumerable<ICliVerb> GetBuiltInVerbs()
        {
            yield break;
          //  yield return new ExitVerb();
          //  yield return new NativeCommandsVerb();
          //  yield return new BatchVerb(this.ExecuteWhenHandles);
          //  yield return new ScriptVerb(this.ExecuteWhenHandles);
        }

        protected sealed override IEnumerable<ICliModule> GetCoreModules()
        {
            return base.GetCoreModules().Concat(GetModules());
        }

        protected virtual IEnumerable<ICliModule> GetModules()
        {
            yield break;
        }









        protected override void Dispose(bool disposing)
        {
            if (this._isDisposed)
                return;
            if (disposing)
            {

                //TODO: Implement other disposing actions.
                base.Dispose(disposing);
            }
            this._isDisposed = true;
        }

    }

}
