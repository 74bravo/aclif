using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CliVerbArgumentAttribute : BaseAttribute
    {
        //private readonly string? longName;
        //private readonly string? shortName;
        //private string setName;
        //private bool flagCounter;
        //private char separator = ' ';
        //private string group = string.Empty;

        private CliVerbArgumentAttribute() : base()
        {

        }

    }
}
