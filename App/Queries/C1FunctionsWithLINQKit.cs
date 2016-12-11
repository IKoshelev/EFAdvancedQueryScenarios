using App.Extensions;
using Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries
{
    public static class C1FunctionsWithLINQKit
    {
        public static void Query1(Database database, IBusinessEntityRepository repo)
        {
            database.LogSQLToFile("C1FunctionsWithLINQKit-query1-log.html", () =>
            {
                var result2 = repo.GetBusinessEntities().Take(100).ToArray();
                result2.SaveAsHtmlTableFile("C1FunctionsWithLINQKit-query1-result.html");
            });
        }
    }
}
