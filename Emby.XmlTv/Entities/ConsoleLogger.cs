using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Patterns.Logging;

namespace Emby.XmlTv.Entities
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message, params Object[] paramList)
        {
            Console.WriteLine(message, paramList);
        }

        public void Error(string message, params Object[] paramList)
        {
            Console.WriteLine(message, paramList);
        }

        public void Warn(string message, params Object[] paramList)
        {
            Console.WriteLine(message, paramList);
        }

        public void Debug(string message, params Object[] paramList)
        {
            Console.WriteLine(message, paramList);
        }

        public void Fatal(string message, params Object[] paramList)
        {
            Console.WriteLine(message, paramList);
        }

        public void FatalException(string message, System.Exception exception, params Object[] paramList)
        {
            Console.WriteLine(message, paramList);
        }

        public void ErrorException(string message, System.Exception exception, params Object[] paramList)
        {
            Console.WriteLine(message, paramList);
        }

        public void LogMultiline(string message, LogSeverity severity, System.Text.StringBuilder additionalContent)
        {
            Console.WriteLine(message);
        }

        public void Log(LogSeverity severity, string message, params Object[] paramList)
        {
            Console.WriteLine(message);
        }
    }
}
