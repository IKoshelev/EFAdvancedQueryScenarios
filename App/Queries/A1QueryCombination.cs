using App.Extensions;
using Repositories;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace App.Queries
{
    public static class A1QueryCombination
    {

        public static void Query1(Database database, IProductsRepository repo)
        {
            database.LogSQLToFile("A1QueryCombination-query1-1-log.html", () =>
            {
                var result1 = repo.GetWorkOrderSummaries().Take(1000).ToArray();
                result1.SaveAsHtmlTableFile("A1QueryCombination-query1-1-result.html");
            });

            database.LogSQLToFile("A1QueryCombination-query1-2-log.html", () =>
            {
                var result2 = repo.GetProductModelOrderStats().ToArray();
                result2.SaveAsHtmlTableFile("A1QueryCombination-query1-2-result.html");
            });
        }

        public static void Query2(Database database, IProductsRepository repo)
        {
            database.LogSQLToFile("A1QueryCombination-query2-1-log.html", () =>
            {
                var result1 = repo.GetFreshProductsWithBadReviews();
                result1.SaveAsHtmlTableFile("A1QueryCombination-query2-1-result.html");
            });        
        }
    }
}
