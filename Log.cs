using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VPScoreHelper
{
    public enum LogLevel     
    {         
        Verbose=0, 
        Info=1, 
        Error=2 
    }

    public class LogEventArgs
    {
        public LogEventArgs(LogLevel level, string text) { Text = text; Level = level; }
        public string Text { get; } 
        public LogLevel Level { get; } 

    }

    public class Log
    {
        public delegate void LogEventHandler(object sender, LogEventArgs e);
        public event LogEventHandler NewLogEntry;
        
        public static Log Instance { 
            get
            {
                if (_instance == null)
                    _instance = new Log();
                return _instance;
            }
        }

        private static Log _instance = null;
        private string _logfileName = "";

        private Log() 
        {
            _logfileName =
                Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                 @"log\"+DateTime.Now.ToString("yyyy-MM") + ".txt");
        }

        public void WriteLine(LogLevel level, string text)
        {
            string l = "";
            switch (level)
            {
                case LogLevel.Info: l = "[I]"; break;
                case LogLevel.Verbose: l = "[V]"; break;
                case LogLevel.Error: l = "[E]"; break;
            }

            string fullText = $"{DateTime.Now.ToString("yyMMdd HH:mm")} {l} {text}";

            string logDir = Path.GetDirectoryName(_logfileName);
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            File.AppendAllText(_logfileName, fullText + Environment.NewLine);

            LogEventHandler temp = NewLogEntry;
            if (temp != null)
            {
                temp(this, new LogEventArgs(level, text));
            }
        }
    }
}
