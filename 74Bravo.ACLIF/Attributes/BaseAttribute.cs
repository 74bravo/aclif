using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{

    public abstract class BaseAttribute : Attribute
    {


        private bool _isempty;


        protected internal BaseAttribute(bool isEmpty = false)
        {
            _isempty = isEmpty;
        }

        public bool IsEmpty => _isempty;

        private string? _helpText;
        public string HelpText
        {
            get => _helpText ??= string.Empty;
            set => _helpText = value ?? string.Empty;
        }

        private string? _description;
        public string Description
        {
            get  => _description ??= string.Empty; 
            set => _description = value ?? string.Empty;
        }



        public bool Hidden
        {
            get;
            set;
        }


    }
}
