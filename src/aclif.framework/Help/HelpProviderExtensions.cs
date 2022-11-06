using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static aclif.CliVerb;

namespace aclif
{

    public static partial class Help
    {

            #region LogHelp General Methods

            public static void LogHelp(this IHelpItem helpItem, int depth = 1)
            {
                if (!helpItem.Hidden) Log.Help(helpItem.HelpFormat, helpItem.HelpArguments);
            }

            public static void LogDescription(this IHelpItem helpItem)
            {
                if (!helpItem.Hidden)
                    Log.Help(Formats.DescriptionFormat, new[] { helpItem.Description });
            }


            public static string Format(this IHelpItem helpItem)
            {
                return String.Format(helpItem.HelpFormat, helpItem.HelpArguments);
            }


            #endregion

            #region LogVerbHelp Methods

            //    public static void LogVerbUsage(this ICliVerb helpItem, string root, string module, string verb, IEnumerable<ArgumentProperty> argDictionary)
            public static ICliVerb LogVerbUsage(this ICliVerb helpItem)
            {
                if (!helpItem.Hidden)
                {

                    Log.Help("\nUsage:\n  {0} <root options> {1} <module options> {2} <options> {3}"
                        , new[] {helpItem.ParentHelpItem?.ParentHelpItem?.HelpLabel ?? string.Empty
                            ,helpItem.ParentHelpItem?.HelpLabel ?? string.Empty
                            ,helpItem.HelpLabel
                            ,helpItem.Arguments.ToUsageHelp() });

                }
                return helpItem;
            }


            public static ICliVerb LogVerbOptions(this ICliVerb helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nVerb Options:");
                    helpItem.LogOptions(helpItem.Switches, helpItem.Options);
                }
                return helpItem;
            }


            public static ICliVerb LogVerbArguments(this ICliVerb helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nVerb Arguments:");
                    helpItem.LogArguments(helpItem.Arguments);
                }
                return helpItem;
            }

            public static ICliVerb LogVerbSubVerbs(this ICliVerb helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nSubverbs:");
                    helpItem.LogVerbs();
                }
                return helpItem;
            }

            #endregion

            #region LogModuleHelp Methods

            public static ICliModule LogModuleUsage(this ICliModule helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nUsage:\n  {0} <root options> {1} <module options> {2} "
                             , new[] {helpItem.ParentHelpItem?.HelpLabel ?? String.Empty
                                 ,helpItem.HelpLabel
                                 ,helpItem.Arguments.ToUsageHelp() });

                }
                return helpItem;
            }

            public static ICliModule LogModuleOptions(this ICliModule helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nModule Options:");
                    helpItem.LogOptions(helpItem.Switches, helpItem.Options);
                }
                return helpItem;
            }

            public static ICliModule LogModuleArguments(this ICliModule helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nModule Arguments:");
                    helpItem.LogArguments(helpItem.Arguments);
                }
                return helpItem;
            }

            public static ICliModule LogModuleVerbs(this ICliModule helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nModule Verbs:");
                    helpItem.LogVerbs();
                }
                return helpItem;
            }

            #endregion


            #region LogRootHelp methods

            public static ICliRoot LogRootUsage(this ICliRoot helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nUsage:\n  {0} <root options> {1}", new[] { helpItem.HelpLabel, helpItem.CliVerbs.ToUsageHelp() });
                }
                return helpItem;
            }

            public static ICliRoot LogRootOptions(this ICliRoot helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nRoot Options:");
                    helpItem.LogOptions(helpItem.Switches, helpItem.Options);
                }
                return helpItem;
            }

            public static ICliRoot LogRootArguments(this ICliRoot helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nRoot Arguments:");
                    helpItem.LogArguments(helpItem.Arguments);
                }
                return helpItem;
            }

            public static ICliRoot LogRootMethods(this ICliRoot helpItem)
            {
                if (!helpItem.Hidden)
                {
                    Log.Help("\nRoot Methods:");
                    helpItem.LogVerbs();
                }
                return helpItem;
            }



            #endregion

            #region LogHelp private / internal methods

            private static void LogOptions(this IHelpItem helpItem, IEnumerable<SwitchProperty> switchDictionary, IEnumerable<OptionProperty> optDictionary)
            {
                if (helpItem.Hidden) return;
                foreach (SwitchProperty item in switchDictionary)
                {
                    item.arg.LogHelp();
                }
                foreach (OptionProperty item in optDictionary)
                {
                    item.arg.LogHelp();
                }
            }

            private static void LogArguments(this IHelpItem helpItem, IEnumerable<ArgumentProperty> argDictionary)
            {
                if (helpItem.Hidden) return;
                foreach (ArgumentProperty item in argDictionary)
                {
                    item.arg.LogHelp();
                }
            }

            public static void LogVerbs(this ICliVerb helpItem)
            {
                if (helpItem.Hidden) return;
                foreach (ICliVerb verb in helpItem.CliVerbs)
                {
                    verb.LogHelp();
                }
            }

            internal static string ToUsageHelp(this IEnumerable<ArgumentProperty> argDictionary, string format = "[{0}]")
            {
                return string.Join(' ', argDictionary.PropertyInfoNames(format));
            }

            internal static string ToUsageHelp(this IEnumerable<ICliVerb> verbs, string format = "[{0}]")
            {
                return string.Join(' ', verbs.PropertyInfoNames(format));
            }

            internal static IEnumerable<string> PropertyInfoNames(this IEnumerable<ArgumentProperty> argDictionary, string format = "[{0}]")
            {
                foreach (var argProp in argDictionary)
                {
                    var lbl = argProp.arg.HelpLabel;
                    if (String.IsNullOrEmpty(lbl))
                        lbl = argProp.pi.Name;

                    yield return string.Format(format, lbl);
                }
            }

            internal static IEnumerable<string> PropertyInfoNames(this IEnumerable<ICliVerb> verbs, string format = "[{0}]")
            {
                foreach (var verb in verbs)
                {
                    var lbl = verb.HelpLabel;
                    if (String.IsNullOrEmpty(lbl))
                        lbl = verb.Verb;

                    yield return string.Format(format, lbl);
                }
            }

            #endregion

            //public static string ToHelpString (this IHelpItem helpItem, int depth = 1)
            //{
            //    if (helpItem.Hidden) return String.Empty;
            //    if (depth == 1) return helpItem.Format();

            //    return helpItem.DeepFormat(depth);
            //}



            //private static string DeepFormat (this IHelpItem helpItem, int depth)
            //{
            //    var sb = new StringBuilder(helpItem.Format());

            //    if (helpItem.HelpLoggers != null)
            //        helpItem.HelpLoggers.DeepFormat(sb, depth-1);

            //    return sb.ToString();

            //}

            //private static void DeepFormat (this IEnumerable<IHelpLogger> helpProviders, StringBuilder sb, int depth)
            //{
            //    //TODO:  Need to go a little more in depth with this command....
            //    if (depth < 1) return;
            //    var nextDepth = depth - 1;
            //    foreach (var helpProvider in helpProviders)
            //    {
            //        sb.Append(helpProvider.LogHelp(nextDepth));
            //    }
            //}

            //public static StringBuilder InitHelpBuilder(IHelpItem helpItem)
            //{
            //    return new StringBuilder(helpItem.ToHelpString());
            //}

            //public static void AppendHelpItem(IHelpItem helpItem, StringBuilder helpBuilder)
            //{

            //}

            //public static string AppendHelpItemS(this IHelpProvider helpProvider, IEnumerable< IHelpItem> helpItems, StringBuilder helpBuilder)
            //{

            //}




    }
}
