using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif.CliProcess
{
    public class CommandLine
    {
        public static readonly char[] MultiCommandDelimeters = new[] { ';', '\n' };

        private CommandLine()
        {
        }

        public string Raw => Environment.CommandLine;

        private IEnumerable<string>? _rawcommandLines;
        public IEnumerable<string> RawCommandLines => _rawcommandLines ??= this.GetRawCommandLines();


        #region Static Methods

        private static CommandLine? _instance;
        public static CommandLine Instance => _instance ??= new CommandLine();

        #endregion

    }

    #region Extensions

    public static class CommandLineExtensions
    {
        internal static IEnumerable<string> GetRawCommandLines(this CommandLine commandLine)
        {
            StringBuilder sb = new StringBuilder(commandLine.Raw);

            do {

                bool InQuotesFlag = false;
                int i = 0;
                for (i = 0; i < sb.Length; ++i)
                {
                    if (sb[i].Equals('"')) InQuotesFlag = !InQuotesFlag;
                    if (!InQuotesFlag && CommandLine.MultiCommandDelimeters.Contains(sb[i]))
                    {
                        break;
                    }
                }
                yield return sb.Remove(0, i + 1).ToString();

            } while (sb.Length > 0);

            yield break;
        }
    }

    #endregion
}
