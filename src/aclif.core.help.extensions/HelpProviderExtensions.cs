using aclif.help.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace aclif.help
{

    public static class extensions
    {

        #region PrimaryHelpMethods

        public static void ShowHelp<SwitchPropType, OptionPropType, ArgumentPropType>(this IHelpItem helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
        {
            if (!helpItem.Hidden)
                    {

                    //Description:
                    helpItem.LogDescription();

                        if (helpItem is ICliRoot<SwitchPropType, OptionPropType, ArgumentPropType>)
                        {

                       ((ICliRoot< SwitchPropType, OptionPropType, ArgumentPropType>)helpItem)
                        .LogRootUsage()
                        .LogRootOptions()
                        .LogRootArguments()
                        .LogRootMethods();
                }
                        else if (helpItem is ICliModule<SwitchPropType, OptionPropType, ArgumentPropType>)
                        {
                    ((ICliModule<SwitchPropType, OptionPropType, ArgumentPropType>)helpItem)
                        .LogModuleUsage()
                        .LogModuleOptions()
                        .LogModuleArguments()
                        .LogModuleVerbs();
                }
                        else if (helpItem is ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType>)
                {
                    ((ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType>)helpItem)
                        .LogVerbUsage()
                        .LogVerbOptions()
                        .LogVerbArguments()
                        .LogVerbSubVerbs();
                }
                }

        }

        #endregion


        #region LogHelp General Methods

        public static void LogHelp(this IHelpItem helpItem, int depth = 1)
        {
            if (!helpItem.Hidden) Log.Help(helpItem.HelpFormat, helpItem.HelpArguments);
        }

        public static void LogHelp(this ICliVerb helpItem, int depth = 1)
        {
            if (!helpItem.Hidden) Log.Help(helpItem.HelpFormat, helpItem.HelpArguments);
        }

        public static void LogHelp<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> helpItem, int depth = 1)
             where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
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
        public static ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogVerbUsage<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
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


        public static ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogVerbOptions<SwitchPropType, OptionPropType, ArgumentPropType>
                 (this ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
        {
            if (!helpItem.Hidden)
            {
                Log.Help("\nVerb Options:");
               helpItem.LogOptions(helpItem.Switches, helpItem.Options);
            }
            return helpItem;
        }


        public static ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogVerbArguments<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
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

        public static ICliModule<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogModuleUsage<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliModule<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
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

        public static ICliModule<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogModuleOptions<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliModule<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
        {
            if (!helpItem.Hidden)
            {
                Log.Help("\nModule Options:");
                helpItem.LogOptions < SwitchPropType, OptionPropType, ArgumentPropType> (helpItem.Switches, helpItem.Options);
            }
            return helpItem;
        }

        public static ICliModule<SwitchPropType, OptionPropType, ArgumentPropType>
            LogModuleArguments<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliModule<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
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

        ////public static ICliRoot LogRootUsage(this ICliRoot helpItem)
        ////{
        ////    if (!helpItem.Hidden)
        ////    {
        ////        Log.Help("\nUsage:\n  {0} <root options> {1}", new[] { helpItem.HelpLabel, helpItem.CliVerbs.ToUsageHelp() });
        ////    }
        ////    return helpItem;
        ////}

        public static ICliRoot<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogRootUsage<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliRoot<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
        {
            if (!helpItem.Hidden)
            {
                Log.Help("\nUsage:\n  {0} <root options> {1}", new[] { helpItem.HelpLabel, helpItem.CliVerbs.ToUsageHelp() });
            }
            return helpItem;
        }

        public static ICliRoot<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogRootOptions<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliRoot<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
        {
            if (!helpItem.Hidden)
            {
                Log.Help("\nRoot Options:");
                helpItem.LogOptions(helpItem.Switches, helpItem.Options);
            }
            return helpItem;
        }

        public static ICliRoot<SwitchPropType, OptionPropType, ArgumentPropType> 
            LogRootArguments<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliRoot<SwitchPropType, OptionPropType, ArgumentPropType> helpItem)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper
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

        private static void LogOptions<SwitchPropType, OptionPropType>(this IHelpItem helpItem, IEnumerable<ISwitchProperty<SwitchPropType>> switchDictionary, IEnumerable<IOptionProperty<OptionPropType>> optDictionary)
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper

        {
            if (helpItem.Hidden) return;
            foreach (ISwitchProperty<SwitchPropType> item in switchDictionary)
            {
                item.arg.LogHelp();
            }
            foreach (IOptionProperty<OptionPropType> item in optDictionary)
            {
                item.arg.LogHelp();
            }
        }


        private static void LogOptions<SwitchPropType, OptionPropType, ArgumentPropType>(this ICliVerb<SwitchPropType, OptionPropType, ArgumentPropType> helpItem, IEnumerable<ISwitchProperty<SwitchPropType>> switchDictionary, IEnumerable<IOptionProperty<OptionPropType>> optDictionary)
            where ArgumentPropType : IHelpItem, IHelper
            where OptionPropType : IHelpItem, IHelper
            where SwitchPropType : IHelpItem, IHelper

        {
            if (helpItem.Hidden) return;
            foreach (ISwitchProperty<SwitchPropType> item in switchDictionary)
            {
                item.arg.LogHelp();
            }
            foreach (IOptionProperty<OptionPropType> item in optDictionary)
            {
                item.arg.LogHelp();
            }
        }

        private static void LogArguments<ArgPropType>(this IHelpItem helpItem, IEnumerable<IArgumentProperty<ArgPropType>> argDictionary)
            where ArgPropType : IHelpItem, IHelper
        {
            if (helpItem.Hidden) return;
            foreach (IArgumentProperty<ArgPropType> item in argDictionary)
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

        internal static string ToUsageHelp<ArgPropType>(this IEnumerable<IArgumentProperty<ArgPropType>> argDictionary, string format = "[{0}]")
            where ArgPropType : IHelpItem, IHelper
        {
            return string.Join(' ', argDictionary.PropertyInfoNames(format));
        }

        internal static string ToUsageHelp(this IEnumerable<ICliVerb> verbs, string format = "[{0}]")
        {
            return string.Join(' ', verbs.PropertyInfoNames(format));
        }

        internal static IEnumerable<string> PropertyInfoNames<ArgPropType>(this IEnumerable<IArgumentProperty<ArgPropType>> argDictionary, string format = "[{0}]")
           where ArgPropType : IHelpItem, IHelper
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

