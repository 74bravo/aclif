using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{

    public abstract class BaseAttribute : Attribute
    {
        private object @default;
        private string hint;
        private string helpText;
        private string description;
        private Type resourceType;


        protected internal BaseAttribute()
        {
            helpText = string.Empty;
            Hint = string.Empty;

            resourceType = null;
        }

        public bool Required
        {
            get;
            set;
        }

        public object Default
        {
            get { return @default; }
            set
            {
                @default = value;
            }
        }


        public string HelpText
        {
            get => helpText ?? string.Empty;
            set => helpText = value ?? throw new ArgumentNullException("value");
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                description = value;
            }
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

        public bool Hidden
        {
            get;
            set;
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
