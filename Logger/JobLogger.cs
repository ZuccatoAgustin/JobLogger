using Logger.Fwk;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Logger
{
    public sealed class JobLogger
    {
        private readonly ILogSourceFactory logSourceFactory;
        private readonly ILogConfiguration logConfiguration;
        private static readonly JobLogger instance;
        private static JobLogger Instance
        {
            get
            {
                return instance;
            }
        }


        private JobLogger()
        {
            logConfiguration = Locator.Resolve<ILogConfiguration>() ?? new DefaultLogConfiguration(); 
            logSourceFactory = new LogSourceFactory();
        }

        static JobLogger()
        {
            instance = new JobLogger();
        }


        public static async Task Info(string message)
        {
            await Instance.Log(message, LogLevel.INFO);
        }

        public static async Task Warning(string message)
        {
            await Instance.Log(message, LogLevel.WARNING);
        }

        public static async Task Error(string message)
        {
            await Instance.Log(message, LogLevel.ERROR);
        }

        private async Task Log(string message, LogLevel level)
        {

            if (!logConfiguration.AllowLevels.HasFlag(level)) return;

            var allowSource =  GetFlags(logConfiguration.AllowSource).ToList();
            foreach (LogSource logsource in allowSource)
            {
               var logsourceInstance = logSourceFactory.Create(logsource);
               await logsourceInstance.Log(message, level);
            }

        }


        private static IEnumerable<Enum> GetFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }

    }
}