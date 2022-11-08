using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.core.interfaces
{
    public interface ICliShellInterface
    {

        public delegate ICliVerbResult HandleInput(string[] args);

        bool IsOpen { get; }

        ICliVerbResult Launch(string prompt, HandleInput handler);

    }
}
