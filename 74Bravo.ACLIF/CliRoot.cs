using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using System.Threading;
using ACLIF.Attributes;
using System.Reflection;
using System.Diagnostics.SymbolStore;
using System.ComponentModel;

namespace ACLIF // Note: actual namespace depends on the project name.
{
    public abstract class CliRoot : CLIModule, ICliRoot, IDisposable
    {
        //private string[] _args;

        private bool _isDisposed;

        protected CliRoot()
        {
        }

        [CliVerbSwitch("--diagnostics", "-d")]
        public bool Diagnostics { get; set; }


        [CliVerbOption("--verbosity", "-v")]
        public Verbosity Verbosity { get; set; }

        protected virtual string CliModuleSearchExpression => "*.climodule.*.dll";

        private CliModuleLoader _cliModuleLoader;
        private CliModuleLoader CliModuleLoader
            {
            get
            {
                if (_cliModuleLoader == null)
                {
                    _cliModuleLoader = new CliModuleLoader(CliModuleSearchExpression);
                }
                return _cliModuleLoader;
            }
         }


        protected static int Go<T> (string[] args) where T : CliRoot, new()
        {
            int resultCode = 1;

            try
            {
                var result = new T().ExecuteWhenHandles(args);
                Console.WriteLine(result.Message);
                return result.ResultCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
              
            }

            return resultCode;
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

        public override string Help => 
            !RootAttribute.IsEmpty
            ? RootAttribute.HelpText
            : throw new NotImplementedException("Help property must be implemented or use CliRoot Attribute");

        private CliRootAttribute _rootAttribute;
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
            //TODO:  Implement Root Level Functionality
            RootPreExecute(args);
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
