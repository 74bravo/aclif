using aclif.Shellio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aclif.Shellio.ShellInterface;

namespace aclif
{
    internal static class Shell
    {

        internal static bool IsOpen { get; private set; }

        internal static ICliVerbResult Launch(string prompt, HandleInput handler)
        {
            IsOpen = true;

            ShellInterface shell = new ShellInterface(prompt, handler);

            var result = shell.Start();

            IsOpen = false;

            return result;

        }

    }
}
