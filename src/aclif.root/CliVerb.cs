using aclif.Attributes;
using aclif.help.Interface;
using aclif.Help;
using aclif.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace aclif
{

    public abstract class CliVerb : CliCoreVerb
    {
        public CliVerb()
        {
           // Console.WriteLine($"Instantiating {this.GetType().FullName}");
        }

        public override string Verb => 
            !VerbAttribute.IsEmpty 
            ? VerbAttribute.Verb 
            : throw new NotImplementedException("Verb property must be implemented or use CliVerb Attribute");
        
        public override string Description => 
            !VerbAttribute.IsEmpty 
            ? VerbAttribute.Description 
            : throw new NotImplementedException("Description property must be implemented or use CliVerb Attribute");

        public override string HelpFormat => 
            !VerbAttribute.IsEmpty 
            ? VerbAttribute.HelpFormat 
            : throw new NotImplementedException("HelpFormat property must be implemented or use CliVerb Attribute");

        public override string HelpLabel =>
             !VerbAttribute.IsEmpty 
            ? String.IsNullOrEmpty(VerbAttribute.HelpLabel)
            ? VerbAttribute.Verb
            : VerbAttribute.HelpLabel
            : Verb;



        //Todo  Initialize Verb Parents

        protected virtual IEnumerable<ICliVerb> GetVerbs()
        {
            yield break;
        }

        private IEnumerable<ICliVerb> GetHelpVerb()
        {
            yield return new HelpVerb(this);
            yield return new CommentVerb();
        }

        internal virtual IEnumerable<ICliVerb> GetBuiltInVerbs()
        {
            yield break;
        }

        protected virtual IEnumerable<ICliVerb> GetInheritedVerbs()
        {
            yield break;
        }

        //public string[]? Arguments { get; protected set; }




        private IEnumerable<ICliVerb>? _cliVerbs;
        public override sealed IEnumerable<ICliVerb> CliVerbs =>
            _cliVerbs ??= GetHelpVerb().Concat(GetBuiltInVerbs()).Concat(GetInheritedVerbs()).Concat(GetVerbs()).SetParents(this);







        private IEnumerable<IHelper>? _helpLoggers;
        public IEnumerable<IHelper> HelpLoggers =>
            _helpLoggers ??= GetHelpLoggers();





        protected virtual IEnumerable<IHelper> GetHelpLoggers()
        {
            yield break;
        }

        //private bool _argMembersLoaded = false;
        //private void LoadArgMembers()
        //{
        //    if (!_argMembersLoaded)
        //    {
        //        foreach (MemberInfo property in this.GetType().GetProperties())
        //        {
        //            var vArg = property.GetCustomAttribute<CliVerbArgumentAttribute>() ?? CliVerbArgumentAttribute.Empty;

        //            //Setting ParentHelpItem
        //            vArg.ParentHelpItem = this;

        //            if (!vArg.IsEmpty)
        //            {
        //                if (vArg is CliVerbOptionAttribute)
        //                {
        //                    var optMbr = new OptionProperty((PropertyInfo)property, (CliVerbOptionAttribute)vArg);
        //                    if (!String.IsNullOrEmpty(optMbr.arg.LongName)) OptionDictionary.Add(optMbr.arg.LongName, optMbr);
        //                    if (!String.IsNullOrEmpty(optMbr.arg.ShortName)) OptionDictionary.Add(optMbr.arg.ShortName, optMbr);
        //                }
        //                else if (vArg is CliVerbSwitchAttribute)
        //                {
        //                    var optMbr = new SwitchProperty((PropertyInfo)property, (CliVerbSwitchAttribute)vArg);
        //                    if (!String.IsNullOrEmpty(optMbr.arg.LongName)) SwitchDictionary.Add(optMbr.arg.LongName, optMbr);
        //                    if (!String.IsNullOrEmpty(optMbr.arg.ShortName)) SwitchDictionary.Add(optMbr.arg.ShortName, optMbr);
        //                }
        //                else
        //                {
        //                    ArgDictionary.Add(new ArgumentProperty((PropertyInfo)property, vArg));
        //                }
        //            }
        //         }
        //    }
        //    _argMembersLoaded = true;
        //}




 

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects)
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        //        // TODO: set large fields to null
        //        disposedValue = true;
        //    }
        //}

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CliVerb()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}


    }
}
