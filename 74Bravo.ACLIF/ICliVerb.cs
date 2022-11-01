using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
    public interface ICliVerb
    {
        string verb { get; }
        bool HandlesCommand(string[] args);
        ICliVerbResult ExecuteWhenHandles(string[] args);

        string Description { get; }
        string Help { get; }


        string[] ProcessCommandOptions(string[] args);
        
    }
}
