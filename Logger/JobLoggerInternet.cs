using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Data.SqlClient;
using System.IO;

namespace JobLoggerIntertent
{
    public class JobLogger
    {
        private readonly string[] _types;
        private readonly List<string> _sources;
        private readonly string _connectionString;
        private readonly string _table;
        private readonly string _name;
        private readonly string _route;

        class Constant
        {
            public const string INFO = "info";
            public const string WARNING = "warning";
            public const string ERROR = "error";
            public const string CONSOLE = "console";
            public const string DATABASE = "database";
            public const string FILE = "file";
        }



        /*
         * JobLooger configuration read from web.config or other xml file.
         * <JobLogger>
         *  <types>
         *      info,warning,error
         *  </types>
         *  <!-- Set sources that you'll use -->
         *  <sources>
         *      <console></console>
         *      <database>
         *          <connection>
         *              ConnectionString
         *          </connection>
         *          <table>
         *              Logs
         *          </table>
         *      </database>
         *      <file>
         *          <!-- by default adds date after name-->
         *          <name>
         *              myLog
         *          </name>
         *          <route>
         *              C:\logs\
         *          </route>
         *      </file>
         *  </sources>
         * </JobLogger>
         */

        public JobLogger(string pathConfig)
        {
            var jobDoc = new XmlDocument();
            jobDoc.Load(pathConfig);
            var types = jobDoc.SelectSingleNode("/JobLogger/types");
            var sourceConsole = jobDoc.SelectSingleNode("/JobLogger/sources/console");
            var sourceDatabase = jobDoc.SelectSingleNode("/JobLogger/sources/database");
            var sourceFile = jobDoc.SelectSingleNode("/JobLogger/sources/file");

            if (types == null)
                throw new Exception("Error 0001: It's necessary configure types logs(info,warning or error)");
            _types = types.Value.Split(',');
            for (var i = 0; i < _types.Count(); i++)
            {
                if (_types[i] == "info" || _types[i] == "warning" || _types[i] == "error")
                    continue;
                throw new Exception("Error 0002: Types only can be info, warning or error");
            }

            _sources = new List<string>();
            if (sourceConsole != null)
                _sources.Add(Constant.CONSOLE);

            if (sourceDatabase != null)
            {
                var connectString = sourceDatabase.SelectSingleNode("/connection");
                var table = sourceDatabase.SelectSingleNode("/table");
                if (connectString == null || table == null)
                {
                    throw new Exception("Error 0003: It's necessary configure connection and table fields");
                }
                _connectionString = connectString.Value;
                _table = table.Value;
                _sources.Add(Constant.DATABASE);
            }

            if (sourceFile != null)
            {
                var name = sourceFile.SelectSingleNode("/name");
                var route = sourceFile.SelectSingleNode("/route");
                if (route == null || name == null)
                {
                    throw new Exception("Error 0004: It's necessary configure route and name fields");
                }
                _name = name.Value;
                _route = route.Value;
                _sources.Add(Constant.FILE);
            }
        }

        public void Info(string message)
        {
            if (!_types.Contains(Constant.INFO)) return;
            SetLog(message, Constant.INFO);
        }

        public void Warning(string message)
        {
            if (!_types.Contains(Constant.WARNING)) return;
            SetLog(message, Constant.WARNING);
        }

        public void Error(string message)
        {
            if (!_types.Contains(Constant.ERROR)) return;
            SetLog(message, Constant.ERROR);
        }

        private void SetLog(string message, string type)
        {
            if (_sources.Contains(Constant.CONSOLE))
            {
                SetColor(type);
                Console.WriteLine(Log(message, type));
            }
            if (_sources.Contains(Constant.DATABASE))
            {
                SetLogDataBase(_connectionString, _table, message, type);
            }
            if (_sources.Contains(Constant.FILE))
            {
                SetLogFile(_name, _route, message, type);
            }
        }

        private static string Log(string message, string type)
        {
            return Date() + " - [" + type.ToUpper() + "]: " + message;
        }

        private static void SetLogDataBase(string connectionString, string table, string message, string type)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                var transaction = connection.BeginTransaction("Insert Logs");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "INSERT INTO " + table + " VALUES ('" + message + "','" + type + "')";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        private static string Date()
        {
            return DateTime.Now.ToShortDateString();
        }

        private void SetLogFile(string name, string route, string message, string type)
        {
            var fullRoute = route + name + Date();
            File.AppendAllText(fullRoute, Log(message, type));
        }

        private static void SetColor(string type)
        {
            switch (type)
            {
                case Constant.INFO:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Constant.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Constant.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
        }
    }
}