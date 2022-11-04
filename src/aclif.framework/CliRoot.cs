using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using System.Threading;
using aclif.Attributes;
using System.Reflection;
using System.Diagnostics.SymbolStore;
using System.ComponentModel;

namespace aclif // Note: actual namespace depends on the project name.
{
    public abstract class CliRoot : CliModule, ICliRoot, IDisposable
    {
        //private string[] _args;

        private bool _isDisposed;

        protected CliRoot()
        {
            Log.Trace($"Instantiating CLIRoot() from {this.GetType().FullName}");
        }

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

        protected static int Go<T> (string[] args) where T : CliRoot, new()
        {
            ICliVerbResult result;
            try
            {
                result = new T().ExecuteWhenHandles(args);
            }
            catch (Exception ex)
            {
               result = VerbResult.Exception(ex);
            }
            finally
            {
              
            }
            Console.WriteLine(result.Message);
            return result.ResultCode;
        }

        //public override ICliVerbResult Execute(string[] args)
        //{
        //  //  throw new NotImplementedException();
        //  // No action at this level..

        //}

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

        internal sealed override void PreExecute(string[] args)
        {

            Log.Verbosity = this.Verbosity;
           
            if (this.Debugging) ConfigureDebugging();

            RootPreExecute(args);
        }

        internal void ConfigureDebugging()
        {
            Log.Information("Initializing Debug Mode");
            Log.Debugging = true;

            Log.Debug("Started");

        }

        protected virtual void RootPreExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void ModulePreExecute(string[] args) { }


        protected override ICliVerbResult Execute(string[] args)
        {
            // do nothing for now...
            return VerbResult.NoAction();
        }

        internal sealed override void PostExecute(string[] args)
        {
            //TODO:  Implement Root Level Functionality
            RootPostExecute(args);
        }

        protected virtual void RootPostExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void ModulePostExecute(string[] args) { }

        protected virtual void Dispose(bool disposing)
        {
            if (this._isDisposed)
                return;
            if (disposing)
            {
                //TODO: Implement other disposing actions.
            }
            this._isDisposed = true;
        }

        public void Dispose()
        {
            this.CliModuleLoader.Dispose();
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

    }
}
