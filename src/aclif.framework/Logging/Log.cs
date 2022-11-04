using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static class  Log
    {

        //private Log() { }

        public static Verbosity Verbosity { get; internal set; }

        public static bool Debugging { get; internal set; } = false;


        //private Log _instance = new Log();

        public static void Trace(string msg)
        {
            if (Verbosity == Verbosity.Diagnostic)
                 Console.WriteLine(msg);
        }

        public static void Debug(string msg)
        {
            if (Debugging)
                Console.WriteLine($"DEBUG: {msg}");
        }

        public static void Information(string msg)
        {
            if (Verbosity >= Verbosity.Normal)
                Console.WriteLine(msg);
        }

        public static void Warning(string msg)
        {
            if (Verbosity >= Verbosity.Normal)
                Console.WriteLine($"WARNING: {msg}");
        }

        public static void Error( string msg, Exception ex)
        {
            //Always Logging an Error

            Console.WriteLine($"ERROR: {msg}");
            Console.WriteLine(ex.InnerException?.Message ?? string.Empty);
        }

        public static void Critical(string msg)
        {
            if (Verbosity >= Verbosity.Minimal)
                Console.WriteLine($"CRITICAL: {msg}");
        }

        public static void Help(string msg)
        {
             Console.WriteLine(msg);
        }

        public static void Help(string format, object[] args)
        {
            Console.WriteLine(format, args);
        }

    }
}
