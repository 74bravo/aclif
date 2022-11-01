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
        //private readonly string? longName;
        //private readonly string? shortName;
        //private string setName;
        //private bool flagCounter;
        //private char separator = ' ';
        //private string group = string.Empty;

        private object @default;
        private string hint;
        private Type resourceType;

        public CliVerbArgumentAttribute() : base()
        {
            Hint = string.Empty;
            resourceType = null;
        }



        public object Default
        {
            get { return @default; }
            set
            {
                @default = value;
            }
        }

        public bool Required
        {
            get;
            set;
        }

        public string Hint
        {
            get { return hint; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                hint = value;
            }
        }


        public Type ResourceType
        {
            get { return resourceType; }
            set
            {
                resourceType = value;
            }
        }

    }
}
