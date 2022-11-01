using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
    public interface ICliModule : ICliVerb
    {

        IEnumerable<ICliVerb> CliVerbs { get; }

    }
}
