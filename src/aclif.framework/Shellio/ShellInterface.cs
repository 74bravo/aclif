using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace aclif.Shellio
{
    public class ShellInterface
    {

        private object shellLock = new object();

        public delegate ICliVerbResult HandleInput(string[] args);

        private HandleInput Invoke;
        private string Prompt;
        public const string exit = "exit";

        internal ShellInterface(string prompt, HandleInput _handleInput)
        {
            Invoke = _handleInput;  
            Prompt = prompt;
        }

        internal ICliVerbResult Start()
        {
            lock (shellLock) 
            { 
                string commandLine = string.Empty;

                ICliVerbResult LastResult = VerbResult.NoAction();

                try
                {

                    StandardIO.NewLine();
                    StandardIO.WriteLine("Shell Mode Started.  Enter {0} to end Shell Mode.", new [] { exit });
                    StandardIO.NewLine();

                    do
                    {
                        StandardIO.NewLine();
                        StandardIO.Write($"{Prompt} {Directory.GetCurrentDirectory()}>");
                        commandLine = StandardIO.ReadLine();

                        Log.Debug("You entered\"{0}\"", new[] { commandLine });

                        var args = CommandLineStringSplitter.Instance
                                     .Split(commandLine).ToArray();

                        LastResult = Invoke(args);


                    } while (commandLine.Trim().ToLower() != exit);

                    return LastResult;
                }
                catch
                {
                    throw;
                }
                finally
                {

                }
            }
        }
    }
}
