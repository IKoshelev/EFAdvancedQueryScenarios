using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries
{
    public static class A1QueryCombination
    {
        public static void Query1(IProductsRepository repo)
        {
            var result1 =  repo.GetWorkOrderSummaries().ToArray();
            var result2 = repo.GetProductModelOrderStats().ToArray();
        }

        public static void Query2(IProductsRepository repo)
        {
            var result1 = repo.ReadGridProducts("frame").ToArray();
            var result2 = repo.ReadGridProducts(null).ToArray();
        }
    }
}
