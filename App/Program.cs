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

            var businessEntityRepio = new Repositories.BusinessEntityRepository(context);

            var productSrv = new Services.ProductsService(productRepo);

            var result = businessEntityRepio.GetBusinessEntityes().ToArray();

        }
    }
}
