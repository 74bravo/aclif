using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace aclif.framework.batching.module.batch
{

    public static partial class Batch

    {
       // public delegate ICliVerbResult HandleBatchCommand(string[] args);

        [Export(typeof(ICliModule))]

        public class BatchVerb : BatchingModuleBase
        {

        //    public static readonly char[] MultiCommandDelimeters = new[] { ';', '\n' };



            //private HandleBatchCommand Invoke;

            //internal BatchVerb(HandleBatchCommand _handleBatchCommand)
            //{
            //    Invoke = _handleBatchCommand;
            //}

            public override string Module => "--batch";

            public override string Description => "Used to run multiple commands at one time";

            [CliVerbArgument]
            public override string? RawCommands { get => base.RawCommands; set => base.RawCommands = value;}


        }
    }
}
