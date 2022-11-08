using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.core
{
    public abstract class CliCoreRoot : CliCoreVerb, ICliRoot<CliVerbSwitchAttribute, CliVerbOptionAttribute, CliVerbArgumentAttribute>
    {

        public CliCoreRoot()
        {
            _root.SetGobalInstance (this);
        }


        [CliVerbSwitch("--diagnostics", "-d")]
        public bool Diagnostics { get; set; }

        [CliVerbSwitch("--debug", Hidden = true)]
        public bool Debugging { get; set; } = false;

        [CliVerbOption("--verbosity", "-v")]
        public Verbosity Verbosity { get; set; }


        #region Hide NotApplicable Verb Overrides

        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed override string Verb => String.Empty;

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void VerbPreExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override void VerbPostExecute(string[] args) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override IEnumerable<ICliVerb> GetCoreVerbs()
        {
            return GetCoreModules().Concat(GetComponentModules());
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal sealed override void PostExecute(string[] args)
        {
            //TODO:  Implement Root Level Functionality
            RootPostExecute(args);
        }

        #endregion

        #region Module Composition

        protected virtual string CliModuleSearchExpression => "*.dll";

        private CliModuleLoader? _cliModuleLoader;
        private CliModuleLoader CliModuleLoader =>
            _cliModuleLoader ??= new CliModuleLoader(CliModuleSearchExpression);

        protected virtual IEnumerable<ICliModule> GetComponentModules()
        {
            foreach (ICliModule cliModule in CliModuleLoader.Collection.CliModules)
            {
                yield return cliModule;
            }
        }

        protected virtual IEnumerable<ICliModule> GetCoreModules()
        {
            yield break;
        }

        #endregion

        public override bool HandlesCommand(string[] args)
        {
            return true;
        }

        internal sealed override void PreExecute(string[] args)
        {
            Log.SetVerbosity(this.Verbosity);
            if (this.Debugging) ConfigureDebugging();
            RootPreExecute(args);
        }


        private bool _debugStarted = false;
        internal void ConfigureDebugging()
        {
            if (!_debugStarted)
            {
                _debugStarted = true;

                //      Log.Information("Initializing Debug Mode");
                Log.StartDebugging();
                //     Log.Debug("Started Debug Mode.");

            }
        }


        protected virtual void RootPreExecute(string[] args) { }



        protected virtual void RootPostExecute(string[] args) { }

        protected bool HandlingIsDelegated { get; private set; } = false;

      //  public ExecuteWhenHandlesDelegate ExecuteDelegate => ExecuteWhenHandlesDelegate;

        public ICliVerbResult ExecuteWhenHandlesDelegate(string[] args)
        {
            try
            {
                HandlingIsDelegated = true;

                return ExecuteWhenHandles(args);

            }
            catch
            {
                throw;
            }
            finally
            {
                HandlingIsDelegated = false;
            }
        }


        protected override ICliVerbResult Execute(string[] args)
        {
      //  if (Shell.IsOpen) return VerbResult.NoAction();

            if (CliModuleLoader.ShellInterface.IsOpen) return VerbResult.NoAction();
            return CliModuleLoader.ShellInterface.Launch("ACLIF", ExecuteWhenHandles);

       // if (Shell.TryLaunch("ACLIF", ExecuteWhenHandles, out ICliVerbResult? verbResult)) return verbResult ?? VerbResult.NoAction();
       // return VerbResult.NoAction();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.CliModuleLoader.Dispose();
                base.Dispose(disposing);
            }

        }

        //protected override ICliVerbResult Execute(string[] args)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
