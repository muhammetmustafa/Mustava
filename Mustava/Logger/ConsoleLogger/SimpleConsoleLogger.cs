using System;

namespace Mustava.Logger.ConsoleLogger
{
    public class SimpleConsoleLogger : IConsoleLogger
    {
        public void Log(string message)
        {
            var str = DateTime.Now.ToString("G") + "::: " + message;
            Console.WriteLine(str);
        }

        public void LogF(string message, params object[] parameters)
        {
            Log(string.Format(message, parameters));
        }
    }
}