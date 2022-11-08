using aclif.Attributes;
using aclif.framework.batching;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.framework.batching.module.script
{

    [Export(typeof(ICliModule))]
    public class ScriptModule : BatchingModuleBase
        {

            public override string Module => "--script";

            public override string Description => "Runs a script of commands";


        // TODO:  RawCommands should return the contents of the ScriptFile property.
        public override string? RawCommands { get => base.RawCommands; set => base.RawCommands = value; }

        [CliVerbArgument]
            public FileInfo? ScriptFile { get; set; }

        }

    }
