using Logger.Fwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger 
{
    public class ConsoleLogSource : LogSourceBase
    {
        public override async Task Log(string message, LogLevel level)
        {
            await Task.Run(() =>
            {
                SetColor(level);
                Console.WriteLine(LogData(message, level));
            });

        }


        private static void SetColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.INFO:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
        }
    }
}
