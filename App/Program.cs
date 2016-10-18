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

            var repo = new Repositories.ProductsRepository(context);

            var srv = new Services.ProductsService(repo);

            var test = repo.GetWorkOrderSummaries().ToArray();

            var req = new GridRequestWithAdditionalPayload<TextSearchPayload>()
            {
                Filter = new GridRequestFilter[]
                {
                    new GridRequestFilter()
                    {
                        PropName = "ModelName",
                        Operand = "Contains",
                        JsonValue = "'Road'"
                    }
                },
                Payload = new TextSearchPayload()
            };

            var result = srv.GetProductsForGrid(req).ToArray();

        }
    }
}
