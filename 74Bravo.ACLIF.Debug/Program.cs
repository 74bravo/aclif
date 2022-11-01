using System;
using System.Runtime.CompilerServices;

namespace ACLIF.Debug 
{
    public class Program : CliRoot
    {
        public override string Description => "Another CLI Framework";

        public override string Help => "Help With the Framework";

        static int Main(string[] args) => CliRoot.Go<Program>(args);

    }
}
