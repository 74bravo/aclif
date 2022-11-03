using ACLIF.Attributes;
using System;
using System.Runtime.CompilerServices;



namespace ACLIF.Debug 
{
    [CliRoot(Description = "Another CLI Framework Console CLI for Debugging",HelpLabel = "74Bravo.ACLIF.Debug")]
    public class Program : CliRoot
    {


        static int Main(string[] args) => CliRoot.Go<Program>(args);


    }

}
