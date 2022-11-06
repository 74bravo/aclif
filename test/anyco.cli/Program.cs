using aclif;
using aclif.Attributes;
using System;
using System.Runtime.CompilerServices;



namespace anyco.cli

{
    [CliRoot(Description = "Another CLI Framework Console CLI for Debugging",HelpLabel = "74Bravo.aclif.Debug")]
    public class Program : CliRoot
    {
       static int Main() 
            => CliRoot.Invoke<Program>();

    }
}
