using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CliModuleAttribute : BaseAttribute
    {

        private readonly string module;


        public CliModuleAttribute(string module) : base()
        {
            if (module == null) throw new ArgumentNullException("module");

            this.module = module;
        }

        public string? Module
        {
            get { return module; }
        }

    }



}
