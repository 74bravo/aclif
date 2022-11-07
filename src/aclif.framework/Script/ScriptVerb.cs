using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{

    public static partial class Script
    {

        public delegate ICliVerbResult HandleScriptCommand(string[] args);

        public class ScriptVerb : CliVerb
        {


            public override string Verb => "--script";

            public override string Description => "Runs a script of commands";


            private HandleScriptCommand Invoke;

            internal ScriptVerb(HandleScriptCommand _handleScriptCommand)
            {
                Invoke = _handleScriptCommand;
            }

            protected override ICliVerbResult Execute(string[] args)
            {
                if (ScriptFile == null) return VerbResult.Exception(new ArgumentException("Script file not specified"));


                if (!Script.Settings.ValidFileExtensions.Contains(ScriptFile.Extension.ToLower()))
                    return VerbResult.Exception(new ArgumentException($"Invalid file Extension.  The only acceptable file extensions are {String.Join(", ", Script.Settings.ValidFileExtensions)}"));

                if (!ScriptFile.Exists) return VerbResult.Exception(new FileNotFoundException($"Specified file does not exist at the following location:  {ScriptFile.FullName}"));

                //TODO:  Open the file and run the commands....




                return VerbResult.Success();

            }

            [CliVerbArgument]
            public FileInfo? ScriptFile { get; set; }


        }

    }
}
