using aclif;
using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{

    public static partial class Script
    { 

        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
        public class ValidScriptFileExtensionAttribute : Attribute
        {
            public ValidScriptFileExtensionAttribute(string extension)
            {
                Add = extension;
            }
            public string Add { get; set; }


        }


        public static IEnumerable<ValidScriptFileExtensionAttribute> GetValidScriptFileExtensionAttribute(this System.AppDomain domain)
        {
            return domain.GetCustomAttributes<ValidScriptFileExtensionAttribute>();
        }

        public static IEnumerable<string> GetValidScriptFileExtensions (this System.AppDomain domain)
        {
            foreach (var attr in domain.GetValidScriptFileExtensionAttribute())
            {
                yield return attr.Add.ToLower().Trim(' ');
            }
            yield break;
        }



    }

}
