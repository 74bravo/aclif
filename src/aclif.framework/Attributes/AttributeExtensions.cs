using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static aclif.Script;

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
