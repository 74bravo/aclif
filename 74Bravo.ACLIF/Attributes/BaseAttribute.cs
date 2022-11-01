using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{

    public abstract class BaseAttribute : Attribute
    {

        private string helpText;
        private string description;



        protected internal BaseAttribute()
        {
            helpText = string.Empty;

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



        public bool Hidden
        {
            get;
            set;
        }


    }
}
