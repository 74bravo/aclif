﻿using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.Diagnostics;
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

        public static int Invoke(this ICliRoot root, string args)
        {
            string[] argsArray = CommandLineStringSplitter.Instance
                                     .Split(args).ToArray();

            return root.Invoke(argsArray);
        }



        public static int InvokeCli<CliRootType>(this string[] args)
            where CliRootType : ICliRoot, new()
             => new CliRootType().Invoke(args);

        public static int InvokeCli<CliRootType>(this Process process)
            where CliRootType : ICliRoot, new()
        {
            return process.Args().InvokeCli<CliRootType>();
        }



        public static string[] Args(this Process process)
        {
            var cl = Environment.CommandLine;

            var args = CommandLineStringSplitter.Instance
             .Split(cl);

            args = args.SkipOne();

            return args.ToArray();

        }

        public static IEnumerable<t> SkipOne<t>(this IEnumerable<t> items)
        {
            return items.SkipOrEmpty(1);
        }

        public static IEnumerable<t> SkipOrEmpty<t>(this IEnumerable<t> items, int skipCount)
        {
            return items.SkipOrEmptyWhen(skipCount, true, 0);
        }

        public static IEnumerable<t> SkipOrEmptyWhen<t>(this IEnumerable<t> items, int skipCount, bool condition, int orElseSkipCount = 0)
        {
            int i = 1;
            int _skipCount = condition ? skipCount : orElseSkipCount;
            foreach (var item in items)
            {
                if (i > _skipCount) yield return item;
                i++;
            }
            yield break;
        }

        public static string CleanFirstArg(this string[] args)
        {
            if (args.Length < 1) return string.Empty;
            return args[0].Trim(' ').ToLower();
        }

        public static string CleanFirstArgFirstChars(this string[] args, int charCount = 1)
        {
            var arg1 = args.CleanFirstArg();
            var cnt = charCount <= arg1.Length ? charCount : arg1.Length;

            return args.CleanFirstArg().Substring(0, cnt);

        }

        public static bool FirstArgStartsWith(this string[] args, char chr)
        {
            return args.CleanFirstArg().StartsWith(chr);
        }

        public static bool FirstArgStartsWith(this string[] args, string str)
        {
            return args.CleanFirstArg().StartsWith(str);
        }

    }
}
