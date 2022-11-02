using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CliVerbArgumentAttribute : BaseAttribute
    {

        internal CliVerbArgumentAttribute(bool isempty) : base(isempty) 
        {
        }

        public CliVerbArgumentAttribute() : this(false) { }

        private object? @default;
        public object? Default
        {
            get => @default; 
            set => @default = value;
        }

        public bool Required
        {
            get;
            set;
        }

        private string? _hint;
        public string Hint
        {
            get => _hint ??= string.Empty;
            set => _hint = value ?? string.Empty;
        }


        private Type? _resourceType;
        public Type ResourceType
        {
            get => _resourceType ??= typeof(string);
            set => _resourceType = value ?? typeof(string);
        }

        private static CliVerbArgumentAttribute? _empty;
        public static CliVerbArgumentAttribute Empty => _empty ??= new CliVerbArgumentAttribute(isempty: true);

    }
}
