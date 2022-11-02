using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CliVerbSwitchAttribute : CliVerbOptionAttributeBase
    {

        internal CliVerbSwitchAttribute(string? longName, string? shortName, bool isempty) : base(longName, shortName, isempty) { }

        internal CliVerbSwitchAttribute(bool isempty) : this(string.Empty, string.Empty, isempty)
        {
        }

        public CliVerbSwitchAttribute(string? longName, string? shortName = null) : this(longName, shortName, false)
        {
        }

        public CliVerbSwitchAttribute()
            : this(string.Empty, string.Empty)
        {
        }

        public CliVerbSwitchAttribute(string longName, char shortName)
            : this(longName, shortName.ToString())
        {
        }

        public CliVerbSwitchAttribute(char shortName)
            : this(string.Empty, shortName.ToString())
        {
        }

        private static CliVerbSwitchAttribute? _empty;
        public static new CliVerbSwitchAttribute Empty => _empty ??= new CliVerbSwitchAttribute(isempty: true);

    }
}
