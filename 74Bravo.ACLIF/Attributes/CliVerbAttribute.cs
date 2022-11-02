using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CliVerbAttribute : BaseAttribute
    {

        private readonly string _verb;

        public CliVerbAttribute(string verb) : this(verb, false) { }

        internal CliVerbAttribute(bool isempty = false) : this(string.Empty, isempty) { }

        internal CliVerbAttribute (string verb, bool isempty) : base(isempty)
        {
            _verb = isempty ? "empty-verb" : verb ?? string.Empty;
        }

        public string Verb
        {
            get { return _verb; }
        }

        private static CliVerbAttribute? _empty;
        public static CliVerbAttribute Empty => _empty ??= new CliVerbAttribute(isempty:true);

    }



}
