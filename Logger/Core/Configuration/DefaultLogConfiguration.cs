using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{

    public class DefaultLogConfiguration : ILogConfiguration
    {
        public DefaultLogConfiguration()
        {
            //TODO: sacar de configuracion;
            _allowSource = LogSource.CONSOLE | LogSource.FILE | LogSource.DATABASE;
            //todo: sacar de configuracion;
            _allowLevels = LogLevel.INFO | LogLevel.WARNING | LogLevel.ERROR;
        }


        public LogLevel _allowLevels;

        public LogLevel AllowLevels
        {
            get { return _allowLevels; }
        }

        public LogSource _allowSource;

        public LogSource AllowSource
        {
            get { return _allowSource; }
        }

    } 
}
