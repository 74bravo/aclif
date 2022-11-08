using aclif.core.attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Build.Utilities.SDKManifest;

namespace aclif.Attributes
{
    public static class Extensions
    {

        public static IEnumerable<AtributeType> GetCustomAttributes<AtributeType>(this System.AppDomain domain)
             where AtributeType : Attribute
        {
            foreach (var assembly in domain.GetAssemblies())
            {
                foreach (var attr in assembly.GetCustomAttributes<AtributeType>())
                {
                    yield return attr;
                }
            }
            yield break;
        }

        public static IEnumerable<RegisterCliShellAttribute> GetRegisterCliShellAttributes(this System.AppDomain domain)
        {
            return domain.GetCustomAttributes<RegisterCliShellAttribute>();
        }

        public static bool TryGetFirstRegisterCliShellAttribute(this System.AppDomain domain, out RegisterCliShellAttribute? registerCliShellAttribute )
        {
            registerCliShellAttribute = domain.GetRegisterCliShellAttributes().FirstOrDefault();
            return registerCliShellAttribute != null;
        }




        //private IEnumerable<string>? _validFileExtensions;
        //public IEnumerable<string> ValidFileExtensions => _validFileExtensions ??= GetValidFileExtensions();

        //private IEnumerable<string> GetValidFileExtensions()
        //{
        //    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        //    {
        //        foreach (var attr in assembly.GetCustomAttributes<ValidScriptFileExtensionAttribute>())
        //        {
        //            yield return attr.Add;
        //        }
        //    }
        //    yield break;
        //}


    }
}
