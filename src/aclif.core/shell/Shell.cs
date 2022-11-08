using aclif.Attributes;
using aclif.core.attributes;
using aclif.core.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace aclif
{
    public static partial class Shell
    {

        internal static bool IsOpen { get; private set; }


        private static ICliShellInterface? Instance;

        internal static bool TryLaunch(string prompt, ICliShellInterface.HandleInput handler, out ICliVerbResult? verbResult)
        {
            IsOpen = true;
            verbResult = null;

            try
            {

                if (AppDomain.CurrentDomain.TryGetShellInterface(out ICliShellInterface? shellInterface))
                {
                    if (shellInterface == null) return false;
                    verbResult = shellInterface.Launch(prompt, handler);
                    return true;
                }

                return false;
            }
            finally
            {
                IsOpen = false;
            }

        }


        internal static bool TryGetShellInterface (this System.AppDomain domain, out ICliShellInterface? shellInterface)
        {
            shellInterface = null;
            if (domain.TryGetFirstRegisterCliShellAttribute(out RegisterCliShellAttribute? registerCliShellAttribute))
            {
                if (registerCliShellAttribute == null) return false;

                if (registerCliShellAttribute.CliShellType is ICliShellInterface)
                {
                    shellInterface = (ICliShellInterface)registerCliShellAttribute.CliShellType.GetConstructor(Array.Empty<Type>()).Invoke(new object[] { });
                    return shellInterface != null;
                }
                else return false;
            }
            else return false;
           
        }

    }
}
