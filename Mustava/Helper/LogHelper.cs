using System;
using System.IO;
using Mustava.Extensions;

namespace Mustava.Helper
{
    public static class LogHelper
    {
        public enum LoggingPlace { File, Console, Void}

        public static string FileName { get; set; }

        public static LoggingPlace WhereToLog { get; set; }
        
        static LogHelper()
        {
            WhereToLog = LoggingPlace.Void;
        }

        public static void Log(string message)
        {
            var str = DateTime.Now.ToString("G") + "::: " + message;

            switch (WhereToLog)
            {
                case LoggingPlace.File:
                    if (!FileName.IsNullOrEmpty())
                    {
                        File.AppendAllText(FileName, str + Environment.NewLine);
                    }
                    break;
                case LoggingPlace.Console:
                    Console.WriteLine(str);
                    break;
                case LoggingPlace.Void:
                    break;
            }

        }

        public static void LogF(string message, params object[] parameters)
        {
            Log(DateTime.Now.ToString("G") + "::: " + string.Format(message, parameters));
        }
    }
}