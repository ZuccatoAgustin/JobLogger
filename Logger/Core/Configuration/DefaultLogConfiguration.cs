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
            //TODO: read from configuration;
            _allowSource = LogSource.CONSOLE | LogSource.FILE; //| LogSource.DATABASE in construction;
            //todo: read from configuration;
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
