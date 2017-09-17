using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class FileLogSource : LogSourceBase
    {
        private string directory;
        private string name;

        public FileLogSource()
        {
            directory = Environment.CurrentDirectory;
            name = "LogData";
        }

        public override async Task Log(string message, LogLevel level)
        {
            await Task.Run(() =>
            {
                var fullRoute  = Path.Combine(directory + "\\" + name + Date());
                File.AppendAllText(fullRoute, LogData(message, level));
            }
           );

        }

    }
}
