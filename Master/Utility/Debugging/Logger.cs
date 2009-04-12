using System;
using System.Text;
using System.IO;

namespace Avalon.Utility.Debugging
{
    public class Logger
    {
        public enum LogLevel : byte
        {
            None = 0,
            Info,
            Access,
            Warning,
            Error,
            Debug
        }

        public static TextWriter LogWriter = Console.Out;
        public static void Log(LogLevel level, string module, string format, params object[] arguments)
        {
            //if (level <= Core.ServerConfig.Log.Level)
            //{
                string timestamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string header = string.Format("{0} | {1,-7} | {2,-15} | ", timestamp, level, module);
                lock (LogWriter) LogWriter.WriteLine(header + format, arguments);
            //}
        }
    }
}
