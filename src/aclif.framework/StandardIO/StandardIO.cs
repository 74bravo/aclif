using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using static System.Net.Mime.MediaTypeNames;

namespace aclif
{
    public class StandardIO
    {

        private object ioLock = new object();


        internal ConsoleKeyInfo ioReadKey()
        {
            lock (ioLock)
            {
                return Console.ReadKey();
            }
        }
        internal async Task<ConsoleKeyInfo> ioReadKeyAsync()
        {
            return await Task.Run(() =>
            {
                return ioReadKey();
            });
        }


        internal int ioRead()
        {
            lock (ioLock)
            {
                return Console.Read();
            }
        }
        internal async Task<int> ioReadAsync()
        {
            return await Task.Run(() =>
            {
                return ioRead();
            });
        }

        internal string ioReadLine()
        {
            lock (ioLock)
            {
                return Console.ReadLine() ?? string.Empty;
            }
        }
        internal async Task<string> ioReadLineAsync()
        {
            return await Task.Run(() =>
            {
                return ioReadLine();
            });
        }


        internal void ioClear()
        {
            lock (ioLock)
            {
                Console.Clear();
            }
        }
        internal async Task ioClearAsync()
        {
            await Task.Run(() =>
            {
                ioClear();
            });
        }


        internal void ioWrite (string text)
        {
            lock (ioLock)
            {
                Console.Write(text);
            }
        }
        internal async Task ioWriteAsync(string text)
        {
            await Task.Run(() =>
               {
                   ioWrite(text);
               });
        }
        internal async Task ioWriteAsync(string format, object[] args)
        {
            await Task.Run(() =>
            {
                ioWrite(string.Format(format, args));
            });
        }


        internal void ioWriteLine(string text)
        {
            lock (ioLock)
            {
                Console.WriteLine(text);
            }
        }
        internal async Task ioWriteLineAsync(string text)
        {
            await Task.Run(() =>
            {
                ioWriteLine(text);
            });
        }
        internal async Task ioWriteLineAsync(string format, object[] args)
        {
            await Task.Run(() =>
            {
                ioWriteLine(string.Format(format, args));
            });
        }


        # region Static Properties & Methods

        private static StandardIO? _instance;
        public static StandardIO Instance => _instance ?? (_instance = new StandardIO());


        #region Static Write

        public static void Write(string text)
        {
            Instance.ioWrite(text);
        }
        public static async Task WriteAync(string text)
        {
            await Instance.ioWriteAsync(text);
        }
        public static void Write(string format, object[] args) 
            => Write(String.Format(format, args));
        public static async Task WriteAync(string format, object[] args)
        {
            await Instance.ioWriteAsync(format, args);
        }

        #endregion


        #region Static WriteLine

        public static void NewLine(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(Environment.NewLine);
            }
        }

        public static void WriteLine(string text)
        {
            Instance.ioWriteLine(text);
        }
        public static async Task WriteLineAsync(string text)
        {
            await Instance.ioWriteLineAsync(text);
        }

        public static void WriteLine(string format, object[] args)
            => Write(String.Format(format, args));

        public static async Task WriteLineAsync(string format, object[] args)
        {
            await Instance.ioWriteLineAsync(format, args);
        }

        #endregion


        #region Static Read

        public static ConsoleKeyInfo ReadKey()
        {
                return Instance.ioReadKey();
        }
        public static async Task<ConsoleKeyInfo> ReadKeyAsync()
        {
            return await Instance.ioReadKeyAsync();
        }
        public static int Read()
        {
                return Instance.ioRead();
        }
        public async Task<int> ReadAsync()
        {
            return await Instance.ioReadAsync();
        }

        public static string ReadLine()
        {
             return Instance.ioReadLine();
        }
        public static async Task<string> ReadLineAsync()
        {
            return await Instance.ioReadLineAsync();
        }

        public static void Clear()
        {
                Instance.ioClear();
        }
        public static async Task ClearAsync()
        {
            await Instance.ioClearAsync();
        }

        #endregion

        #endregion

    }
}
