using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CliVerbOptionAttribute : CliVerbOptionAttributeBase
    {

        //private string _setName;
        //private bool flagCounter;

        // private string _group = string.Empty;

        internal CliVerbOptionAttribute(string? longName, string? shortName, bool isempty) : base(longName, shortName, isempty) { }

        internal CliVerbOptionAttribute(bool isempty) : this(string.Empty, string.Empty, isempty)
        {
        }

        public CliVerbOptionAttribute(string? longName, string? shortName = null) : this(longName, shortName, false)
        {
        }

        public CliVerbOptionAttribute()
            : this(string.Empty, string.Empty)
        {
        }

        public CliVerbOptionAttribute(string longName, char shortName)
            : this(longName, shortName.ToString())
        {
        }

        public CliVerbOptionAttribute(char shortName)
            : this(string.Empty, shortName.ToString())
        {
        }

        ///// <summary>
        ///// Gets or sets the option's mutually exclusive set name.
        ///// </summary>
        //public string SetName
        //{
        //    get { return setName; }
        //    set
        //    {
        //        if (value == null) throw new ArgumentNullException("value");

        //        setName = value;
        //    }
        //}

        ///// <summary>
        ///// If true, this is an int option that counts how many times a flag was set (e.g. "-v -v -v" or "-vvv" would return 3).
        ///// The property must be of type int (signed 32-bit integer).
        ///// </summary>
        //public bool FlagCounter
        //{
        //    get { return flagCounter; }
        //    set { flagCounter = value; }
        //}



        private char _separator = ' ';
        /// <summary>
        /// When applying attribute to <see cref="IEnumerable{T}"/> target properties,
        /// it allows you to split an argument and consume its content as a sequence.
        /// </summary>
        public char Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }

        private bool? _argValueIsNext;
        public bool ArgValueIsNext => _argValueIsNext ??= _separator == ' ';

        ///// <summary>
        ///// Gets or sets the option group name. When one or more options are grouped, at least one of them should have value. Required rules are ignored.
        ///// </summary>
        //public string Group
        //{
        //    get { return group; }
        //    set { group = value; }
        //}

        private static CliVerbOptionAttribute? _empty;
        public static new CliVerbOptionAttribute Empty => _empty ??= new CliVerbOptionAttribute(isempty: true);

    }
}
