using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    [Flags]
    public enum LogSource
    {     
        CONSOLE = 1,
        DATABASE = 2,
        FILE = 4
    }
}
