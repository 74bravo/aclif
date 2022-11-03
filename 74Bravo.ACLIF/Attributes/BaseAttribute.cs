using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ACLIF.Help;

namespace ACLIF.Attributes
{

    public abstract class BaseAttribute : Attribute, IHelper, IHelpItem
    {

        private bool _isempty;


        protected internal BaseAttribute(bool isEmpty = false)
        {
            _isempty = isEmpty;
        }

        public bool IsEmpty => _isempty;

        private string? _helpLabel;
        public virtual string HelpLabel
        {
            get => _helpLabel ??= string.Empty;
            set => _helpLabel = value ?? string.Empty;
        }

        private string? _helpFormat;
        public string HelpFormat
        {
            get => _helpFormat ??= DefaultHelpFormat;
            set => _helpFormat = value ?? DefaultHelpFormat;
        }

        protected virtual string DefaultHelpFormat => "{0}";



        public virtual string[] HelpArguments => new string[] { Description };

        private string? _description;
        public string Description
        {
            get => _description ??= string.Empty;
            set => _description = value ?? string.Empty;
        }

        public bool Hidden
        {
            get;
            set;
        }

        public CultureInfo HelpCulture => CultureInfo.InvariantCulture;

        public IEnumerable<IHelper> HelpLoggers {get { yield break; } }

        public IHelpItem? ParentHelpItem { get;  set; }

        public virtual void Help(int depth)
        {
            this.LogHelp(depth);
        }


    }
}
