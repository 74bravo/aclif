using aclif.core.attributes;
using aclif.core.interfaces;
using aclif.framework.shell;
using System;
using System.Collections.Generic;
using System.CommandLine.Parsing;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: RegisterCliShell(typeof(ICliShellInterface))]

namespace aclif.framework.shell
{
    [Export(typeof(ICliShellInterface))]
    public class ShellInterface : ICliShellInterface
        {

            private object shellLock = new object();

            private ICliShellInterface.HandleInput? Invoke;
            private string? Prompt;
            public const string exit = "exit";

            public bool IsOpen { get; private set; }

            public ShellInterface()
             {

             }

        internal ShellInterface(string prompt, ICliShellInterface.HandleInput _handleInput) : base()
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
                        StandardIO.WriteLine("Shell Mode Started.  Enter {0} to end Shell Mode.", new[] { exit });
                        StandardIO.NewLine();

                        do
                        {
                            StandardIO.NewLine();
                            StandardIO.Write($"{Prompt} {Directory.GetCurrentDirectory()}> ");
                            commandLine = StandardIO.ReadLine();

                            Log.Debug("You entered\"{0}\"", new[] { commandLine });

                            var args = CommandLineStringSplitter.Instance
                                         .Split(commandLine).ToArray();

                            LastResult = Invoke(args);

                            StandardIO.NewLine();

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

            public ICliVerbResult Launch(string prompt, ICliShellInterface.HandleInput handler)
            {
                try { 
                    Prompt = prompt;
                    Invoke = handler;
                    IsOpen = true;
                    return Start();
                }
                finally
                { 
                    IsOpen = false;
                }

            }
        }

    }

