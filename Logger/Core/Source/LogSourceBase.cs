using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger 
{
    internal abstract class LogSourceBase : ILogSource
    { 
        protected string Date()
        {
            return DateTime.Now.ToString("dd-MM-yyyy");
        }

        protected string LogData(string message, LogLevel level)
        {
            return Date() + " - [" + level.ToString().ToUpper() + "]: " + message;
        }

        public abstract Task Log(string message, LogLevel level);
         
    }
}
