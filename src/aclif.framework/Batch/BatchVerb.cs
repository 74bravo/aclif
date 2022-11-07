using aclif.Attributes;
using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static aclif.Shell;

namespace aclif
{

    public static partial class Batch

    {
        public delegate ICliVerbResult HandleBatchCommand(string[] args);


        internal class BatchVerb : CliVerb
        {

            public static readonly char[] MultiCommandDelimeters = new[] { ';', '\n' };



            private HandleBatchCommand Invoke;

            internal BatchVerb(HandleBatchCommand _handleBatchCommand)
            {
                Invoke = _handleBatchCommand;
            }

            public override string Verb => "--batch";

            public override string Description => "Used to run multiple commands at one time";

            [CliVerbArgument]
            public string? RawCommands { get; set; }

            private bool _inBatchMode = false;
            public override bool HandlesCommand(string[] args)
            {
                if (_inBatchMode) return false;
                return base.HandlesCommand(args);
            }
            protected override ICliVerbResult Execute(string[] args)
            {
                _inBatchMode = true;

                ICliVerbResult LastResult = VerbResult.NoAction();

                try
                {
                    var lines = new List<string>();

                    StringBuilder sb1 = new StringBuilder(RawCommands);

                    //Gathering the command lines....

                    do
                    {

                        var clc = new List<char>();


                        bool InQuotesFlag = false;
                        int i = 0;
                        for (i = 0; i < sb1.Length; ++i)
                        {
                            if (sb1[i].Equals('"')) InQuotesFlag = !InQuotesFlag;
                            if (!InQuotesFlag)
                            {
                                if (MultiCommandDelimeters.Contains(sb1[i]))
                                {
                                    break;
                                }
                                clc.Add(sb1[i]);
                            }
                        }

                        if (clc.Count > 0) lines.Add(String.Concat(clc));

                        sb1 = sb1.Remove(0, i + 1);


                    } while (sb1.Length > 0);

                    // executing each command line


                    foreach (string line in lines)
                    {


                        Log.Debug("Running Batch Command \"{0}\"", new[] { line });

                        var batchCommandArgs = CommandLineStringSplitter.Instance
                                     .Split(line).ToArray();

                        LastResult = Invoke(batchCommandArgs);

                        StandardIO.NewLine();

                    }
                    return LastResult;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    _inBatchMode = false;
                }

            }
        }
    }
}
