using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF.Attributes
{
    public abstract class CliVerbOptionAttributeBase : CliVerbArgumentAttribute
    {
        private readonly string _longName;
        private readonly string _shortName;
        //private string _setName;
        //private bool flagCounter;

        // private string _group = string.Empty;


        internal CliVerbOptionAttributeBase(string? longName, string? shortName, bool isempty) : base(isempty)
        {
            _shortName = shortName ?? string.Empty;
            _longName = longName ?? string.Empty;
        }


        public string LongName
        {
            get { return _longName; }
        }

        public string ShortName
        {
            get { return _shortName; }
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


        ///// <summary>
        ///// Gets or sets the option group name. When one or more options are grouped, at least one of them should have value. Required rules are ignored.
        ///// </summary>
        //public string Group
        //{
        //    get { return group; }
        //    set { group = value; }
        //}

        //private static CliVerbOptionAttributeBase? _empty;
        //public static new CliVerbOptionAttributeBase Empty => _empty ??= new CliVerbOptionAttributeBase(isempty: true);

    }
}
