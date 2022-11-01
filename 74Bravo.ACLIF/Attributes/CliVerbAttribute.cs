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

        private readonly string verb;


        private CliVerbAttribute(string verb) : base()
        {
            if (verb == null) throw new ArgumentNullException("verb");

            this.verb = verb;
        }

        public string? Verb
        {
            get { return verb; }
        }

    }



}
