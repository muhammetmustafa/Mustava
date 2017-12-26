using System;
using System.IO;
using Mustava.Extensions;

namespace Mustava.Logger.FileLogger
{
    public class SimpleFileLogger : IFileLogger
    {
        public string FileName { get; set; }

        public SimpleFileLogger(string fileName)
        {
            FileName = fileName;
        }

        public void Log(string message)
        {
            var str = DateTime.Now.ToString("G") + "::: " + message;
            
            if (!FileName.ExIsNullOrEmpty())
            {
                File.AppendAllText(FileName, str + Environment.NewLine);
            }
        }

        public void LogF(string message, params object[] parameters)
        {
            Log(string.Format(message, parameters));
        }
    }
}