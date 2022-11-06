using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using System.Threading;
using aclif.Attributes;
using System.Reflection;
using System.Diagnostics.SymbolStore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static aclif.Shell;
using aclif.Host;
using static aclif.Batch;
using System.CommandLine.Parsing;
using System.Diagnostics;

namespace aclif // Note: actual namespace depends on the project name.
{
    public class CliRoot : CliModule, ICliRoot, IDisposable
    {
        //private string[] _args;

        private bool _isDisposed;

        public CliRoot() : base()
        {
            Log.Trace($"Instantiating CLIRoot() from {this.GetType().FullName}");
        }

        public static int Invoke()
         => Invoke<CliRoot>();

        public static int Invoke<CliRootType>()
            where CliRootType : ICliRoot, new()
        => Process.GetCurrentProcess().InvokeCli<CliRootType>();


        [CliVerbSwitch("--diagnostics", "-d")]
        public bool Diagnostics { get; set; }

        [CliVerbSwitch("--debug", Hidden = true)]
        public bool Debugging { get; set; } = false;

        [CliVerbOption("--verbosity", "-v")]
        public Verbosity Verbosity { get; set; }

        protected virtual string CliModuleSearchExpression => "*.dll";

        private CliModuleLoader? _cliModuleLoader;
        private CliModuleLoader CliModuleLoader =>
            _cliModuleLoader ??= new CliModuleLoader(CliModuleSearchExpression);

        public sealed override string Module => "";

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

        public override bool HandlesCommand(string[] args)
        {
            return true;
        }

        protected override IEnumerable<ICliVerb> GetVerbs()
        {
            foreach (ICliModule cliModule in CliModuleLoader.Collection.CliModules)
            {
                yield return cliModule;
            }
        }

        internal override IEnumerable<ICliVerb> GetBuiltInVerbs()
        {
            yield return new ExitVerb();
            yield return new NativeCommandsVerb();
            yield return new BatchVerb(this.ExecuteWhenHandles);
        }

        internal sealed override void PreExecute(string[] args)
        {
            Log.Verbosity = this.Verbosity;
            if (this.Debugging) ConfigureDebugging();
            RootPreExecute(args);
        }

        private bool _debugStarted = false;
        internal void ConfigureDebugging()
        {
            if (!_debugStarted) 
            { 
                _debugStarted = true; 

            Log.Information("Initializing Debug Mode");
            Log.Debugging = true;
            Log.Debug("Started Debug Mode.");

            }
        }

        protected virtual void RootPreExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void ModulePreExecute(string[] args) { }


        protected override ICliVerbResult Execute(string[] args)
        {
            if (Shell.IsOpen) return VerbResult.NoAction();
            return Shell.Launch("ACLIF", ExecuteWhenHandles);
        }

        internal sealed override void PostExecute(string[] args)
        {
            //TODO:  Implement Root Level Functionality
            RootPostExecute(args);
        }

        protected virtual void RootPostExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void ModulePostExecute(string[] args) { }

        protected override void Dispose(bool disposing)
        {
            if (this._isDisposed)
                return;
            if (disposing)
            {
                this.CliModuleLoader.Dispose();
                //TODO: Implement other disposing actions.
            }
            this._isDisposed = true;
        }

    }

}
