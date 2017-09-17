using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger 
{
    public class DatabaseLogSource : LogSourceBase
    {
        public override async Task Log(string message, LogLevel level)
        {
            await Task.Run(() =>
            {
                throw new NotImplementedException("Call DB");
            }
           );

        }


    }
}
