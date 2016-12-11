using ColorCode;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class EFDatabaseEtensions
    {
        public static void LogSQLToFile(this Database database, string fileName, Action action)
        {
            var log = "";
            Action<string> logger = (x) =>
            {
                log += new CodeColorizer().Colorize(x, Languages.Sql);
            };

            database.Log += logger;

            try
            {
                action();
            }
            finally
            {
                FileSaver.SaveStringToFile(fileName, log);
                database.Log -= logger;
            }

        }
    }
}
