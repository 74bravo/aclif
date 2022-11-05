using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.Host
{
    public class NativeCommandsVerb : CliSimpleVerb
    {

        public readonly string[] nativeCommands = { "cmd", "cd", "mkdir", "echo", "dir", "pac" };

        public override string Verb => "cmd";


        public override bool HandlesCommand(string[] args)
        {
            if (args == null) return false;
            return nativeCommands.Contains(args[0].Trim(' ').ToLower());
        }

        public override string Description => "Used to exectute a native commands";

        private string _instanceCommand = string.Empty;

        protected override ICliVerbResult Execute(string[] args3)
        {


            var firstArg = args3[0].Trim(' ').ToLower();

            var newArgs = firstArg == Verb ? args3.Skip(1).ToArray() : args3;

            if (newArgs.Length > 0)
            {
                if (firstArg == "cd" || newArgs[0].Trim(' ').ToLower() == "cd")
                {
                    var cdDir = firstArg == "cd" ? args3[1] : newArgs[1];

                    var newPath = Path.GetFullPath(cdDir);
                    System.IO.Directory.SetCurrentDirectory(newPath);

                    return VerbResult.Success();

                }
                else
                { 
                var arguments = String.Join(' ', newArgs);
                var filename = "cmd.exe";

                var process = new Process();

                process.StartInfo.FileName = filename;
                if (!string.IsNullOrEmpty(arguments))
                {
                    process.StartInfo.Arguments = $"/c {arguments} 2>&1";
                }

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.UseShellExecute = false;

                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                var stdOutput = new StringBuilder();
                process.OutputDataReceived += (sender, args) =>
                {

                    stdOutput.AppendLine(args.Data); // Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.
                };

                string? stdError = null;
                try
                {
                    process.Start();
                    process.BeginOutputReadLine();
                    stdError = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                }
                catch (Exception e)
                {
                    throw new Exception("OS error while executing " + String.Format(filename, arguments) + ": " + e.Message, e);
                }

                if (process.ExitCode == 0)
                {
                    StandardIO.WriteLine( stdOutput.ToString());
                }
                else
                {
                    var message = new StringBuilder();

                    if (!string.IsNullOrEmpty(stdError))
                    {
                        message.AppendLine(stdError);
                    }

                    if (stdOutput.Length != 0)
                    {
                        message.AppendLine("Std output:");
                        message.AppendLine(stdOutput.ToString());
                    }

                    throw new Exception(String.Format(filename, arguments) + " finished with exit code = " + process.ExitCode + ": " + message);
                }

                return VerbResult.Success();

                }
            }

            return VerbResult.NoAction();

        }
    }
}
