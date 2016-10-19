using App.Queries;
using Newtonsoft.Json;
using Services.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EFDataContext.AdventureWorksDataContext();

            context.Database.Log = Console.Write;

            var productRepo = new Repositories.ProductsRepository(context);

            var businessEntityRepo = new Repositories.BusinessEntityRepository(context);

            var productSrv = new Services.ProductsService(productRepo);

            A1QueryCombination.Query1(productRepo);
            A1QueryCombination.Query2(productRepo);

            B1DataGridRequests.Query1(productSrv);
            B1DataGridRequests.Query2(productSrv);
            B1DataGridRequests.Query3(productSrv);
            B1DataGridRequests.Query4(productSrv);

            C1FunctionsWithLINQKit.Query1(businessEntityRepo);
        }
    }
}
