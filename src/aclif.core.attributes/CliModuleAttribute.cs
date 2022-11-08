using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CliModuleAttribute : BaseAttribute
    {

        private readonly string _module;


        public CliModuleAttribute(string module) : this(module, false) { }

        internal CliModuleAttribute(bool isempty = false) : this (string.Empty, isempty) { }

        internal CliModuleAttribute(string module, bool isempty) : base(isempty)
        {
            _module = isempty ? "empty-module" : module ?? string.Empty;
        }

        public string Module
        {
            get { return _module; }
        }

        private static CliModuleAttribute? _emptyCliModuleAttribute;
        public static CliModuleAttribute Empty =>
            _emptyCliModuleAttribute ??= new CliModuleAttribute(true);

    }
}
