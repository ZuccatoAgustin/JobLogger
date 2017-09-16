using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    [Flags]
    public enum LogLevel
    {
        INFO = 1,
        WARNING = 2,
        ERROR = 4
    }
}
