using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static class ICliRootExtensions
    {
        public static int Invoke(this ICliRoot root, string[] args) 
        {
            ICliVerbResult result = VerbResult.NoAction();
            try
            {
                var rootType = root.GetType();
                result = root.ExecuteWhenHandles(args);
            }
            catch (Exception ex)
            {
                result = VerbResult.Exception(ex);
            }
            finally
            {
                root.Dispose();
            }
            Log.Information(result.Message);
            return result.ResultCode;
        }

        public static int InvokeCli(this string[] args)
            => args.InvokeCli<CliRoot>();

        public static int InvokeCli<CliRootType>(this string[] args)
            where CliRootType : ICliRoot, new()
             => new CliRootType().Invoke(args);
    }
}
