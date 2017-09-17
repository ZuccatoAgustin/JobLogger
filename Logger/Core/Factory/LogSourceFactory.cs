using Logger.Fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class LogSourceFactory : GenericAbstractFactory<LogSource, LogSourceBase>, ILogSourceFactory
    {
        public LogSourceFactory()
        {
            this.Add<ConsoleLogSource>(LogSource.CONSOLE);
            this.Add<DatabaseLogSource>(LogSource.DATABASE);
            this.Add<FileLogSource>(LogSource.FILE);
        }  
    }
}
