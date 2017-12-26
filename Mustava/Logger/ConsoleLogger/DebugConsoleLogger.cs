using System;
using System.Diagnostics;

namespace Mustava.Logger.ConsoleLogger
{
    public class DebugConsoleLogger : IConsoleLogger
    {
        public void Log(string message)
        {
            var str = DateTime.Now.ToString("G") + "::: " + message;
            Debug.WriteLine(str);
        }

        public void LogF(string message, params object[] parameters)
        {
            Log(string.Format(message, parameters));
        }
    }
}