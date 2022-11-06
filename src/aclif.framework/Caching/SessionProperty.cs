using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static aclif.Caching;

namespace aclif
{
    public static partial class Caching
    {
        public abstract class SessionProperty<PropertyType>
        // where PropertyType :  ISerializable
        {

            internal SessionProperty(PropertyType value)
            {
                Value = value;
            }

            public PropertyType? Value { get; private set; }

            public override string ToString()
            {
                return Value?.ToString() ?? string.Empty;
            }

        }

    }


        public sealed class Session<PropertyType> : SessionProperty<PropertyType>
        //  where PropertyType : ISerializable, new()
        {
            private Session(PropertyType value) : base(value) { }

            public static implicit operator Session<PropertyType>(PropertyType value)
            {
                return new Session<PropertyType>(value);
            }

            public static implicit operator PropertyType(Session<PropertyType> value)
            {
                return value.Value;
            }

        }
    }