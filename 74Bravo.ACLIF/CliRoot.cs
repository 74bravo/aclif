using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;
using System.Threading;

namespace ACLIF // Note: actual namespace depends on the project name.
{
    public abstract class CliRoot : CLIModule, ICliRoot, IDisposable
    {
        //private string[] _args;

        private bool _isDisposed;

        protected CliRoot()
        {
        }

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

        public override string Module => "";

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
        protected override ICliVerbResult Execute(string[] args)
        {
            // do nothing
            return new VerbResult(false, "No Action Taken", 1);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._isDisposed)
                return;
            if (disposing)
            {
                //AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(this.AppDomain_OnUnhandledException);
                //Interlocked.Decrement(ref Session._instanceCount);
                //LogManager.Shutdown();
                //this._moduleLoader?.Dispose();
                //this._services?.Dispose();
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
