using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
    public static class StringToTypeExtension
    {
        public static object? FromString(this Type type, string stringValue, CultureInfo? cultureInfo = null)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return type.Default2();
            }

            Type? valueType = type;

            // If the TValue is a nullable type, get the underlying type.
            if (valueType.GetTypeInfo().IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                valueType = Nullable.GetUnderlyingType(valueType);
            }

            try
            {
                if (valueType == typeof(String)) return stringValue;

                if (typeof(Enum).IsAssignableFrom(valueType)) return Enum.Parse(valueType, stringValue, true);

                if (valueType == typeof(Guid)) return new Guid(stringValue);

                MethodInfo? parseMethodWithCulture = valueType?.GetMethod("Parse", new[] { typeof(string), typeof(CultureInfo) });

                if (parseMethodWithCulture != null)
                {
                    if (parseMethodWithCulture.IsStatic && parseMethodWithCulture.ReturnType == valueType)
                    {
                        return parseMethodWithCulture.Invoke(null, new object[] { stringValue, cultureInfo ?? CultureInfo.InvariantCulture });
                    }
                }

                MethodInfo? parseMethodJustString = valueType?.GetMethod("Parse", new[] { typeof(string) });

                if (parseMethodJustString != null)
                {
                    if (parseMethodJustString.IsStatic && parseMethodJustString.ReturnType == valueType)
                    {
                        return parseMethodJustString.Invoke(null, new object[] { stringValue });
                    }
                }

                ConstructorInfo? constructor = valueType?.GetConstructor(new [] { typeof(string) });

                if (constructor != null)
                {
                    return constructor.Invoke(new object[] { stringValue });
                }
            }
            catch (FormatException ex)
            {
                throw new Exception($"Cannot convert string value \"{stringValue}\" to {valueType?.Name}",ex);
            }

            return new InvalidCastException ($"Cannot convert string value \"{stringValue}\" to {valueType?.Name}");

        }

        public static object? Default2 (this Type type)
        {
            if (type.IsValueType && Nullable.GetUnderlyingType(type) == null)
                return Activator.CreateInstance(type);

            return null;
        }

    }
}
