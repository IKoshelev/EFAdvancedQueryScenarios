using App.Queries;
using App.Basics;
using ColorCode;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new EFDataContext.AdventureWorksDataContext();

            var productRepo = new Repositories.ProductsRepository(context);

            var businessEntityRepo = new Repositories.BusinessEntityRepository(context);

            var productSrv = new Services.ProductsService(productRepo);

            A1QueryCombination.Query1(context.Database, productRepo);
            A1QueryCombination.Query2(context.Database, productRepo);

            // interlude with a short demo of expression trees
            ExpressionTreeBasics.Demo();

            B1DataGridRequests.Query1(context.Database, productSrv);
            B1DataGridRequests.Query2(context.Database, productSrv);
            B1DataGridRequests.Query3(context.Database, productSrv);
            B1DataGridRequests.Query4(context.Database, productSrv);

            C1FunctionsWithLINQKit.Query1(context.Database, businessEntityRepo);
        }
    }
}
