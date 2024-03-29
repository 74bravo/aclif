﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static class  Log
    {

        public static Verbosity Verbosity { get; internal set; }


        public static void SetVerbosity( Verbosity verbosity)
        {
            Verbosity = verbosity;
        }

        public static bool Debugging { get; internal set; } = false;

        public static void Trace(string msg)
        {
            if (Verbosity == Verbosity.Diagnostic)
                 StandardIO.WriteLine(msg);
        }

        public static void Debug(string msg)
        {
            if (Debugging)
                StandardIO.WriteLine($"DEBUG: {msg}");
        }

        public static void Debug(string format, object[] args)
        {
            if (Debugging)
               Debug (String.Format(format,args));
        }

        public static void Information(string msg)
        {
            if (Verbosity >= Verbosity.Normal)
                StandardIO.WriteLine(msg);
        }

        public static void Warning(string msg)
        {
            if (Verbosity >= Verbosity.Normal)
                StandardIO.WriteLine($"WARNING: {msg}");
        }

        public static void Error( string msg, Exception ex)
        {
            //Always Logging an Error

            StandardIO.WriteLine($"ERROR: {msg}");
            StandardIO.WriteLine(ex.InnerException?.Message ?? string.Empty);
        }

        public static void Critical(string msg)
        {
            if (Verbosity >= Verbosity.Minimal)
                StandardIO.WriteLine($"CRITICAL: {msg}");
        }

        public static void Help(string msg)
        {
            StandardIO.WriteLine(msg);
        }
        public static async Task HelpAsync(string msg)
        {
            await StandardIO.WriteLineAsync(msg);
        }

        public static void Help(string format, object[] args)
        {
            StandardIO.WriteLine(format, args);
        }

        public static async Task HelpAsync(string format, object[] args)
        {
            await StandardIO.WriteLineAsync(format, args);
        }

        public static void StartDebugging()
        {

            Log.Information("Initializing Debug Mode");
            Debugging = true;
            Log.Debug("Started Debug Mode.");

        }

        public static void StopDebugging()
        {
            Debugging = false;
        }

        public static void ToggleDebugging()
        {
            Debugging = !Debugging;
        }


    }
}
