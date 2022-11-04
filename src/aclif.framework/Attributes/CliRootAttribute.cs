using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CliRootAttribute : BaseAttribute
    {

        public CliRootAttribute() : base() { }

        internal CliRootAttribute(bool isempty = false) : base(isempty) { }


        private static CliRootAttribute? _empty;
        public static CliRootAttribute Empty => _empty ??= new CliRootAttribute(isempty: true);

    }



}
