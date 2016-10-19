using EFDataContext;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Repositories.Test.Util;

namespace Repositories.Test
{
    [TestClass]
    public class ProductRepositoryTest: DataTestBase
    {
        [TestMethod]
        public void GetWorkOrderSummaries_Test()
        {
            var context = Context();
            context.ProductModels = Set(new ProductModel()
            {
                ProductModelId = 2,
                Name = "MODELNAME"
            });

            context.Products = Set(new Product()
            {
                ProductModelId = 2,
                ProductId = 3,
                Name = "PRODUCTNAAME"
            });

            context.WorkOrders = Set(new WorkOrder()
            {
                ProductId = 3,
                WorkOrderId = 4,
                EndDate = DateTime.Today,
                StartDate = DateTime.Today.AddDays(-5)
            });

            context.WorkOrderRoutings = Set(new WorkOrderRouting()
            {
                WorkOrderId = 4,
                LocationId = 5
            },
            new WorkOrderRouting()
            {
                WorkOrderId = 4,
                LocationId = 6
            });

            context.Locations = Set(new Location()
            {
                LocationId = 5,
                Name = "LOCATION5"
            },
            new Location()
            {
                LocationId = 6,
                Name = "LOCATION6"
            });

            var repo = new ProductsRepository(context);

            var result = repo
                .GetWorkOrderSummaries()
                .FixDbFunctionCalls()
                .ToArray();

            Assert.IsTrue(result.Count() == 1);
            var item = result[0];
            Assert.IsTrue(item.ModelName == "MODELNAME");
            Assert.IsTrue(item.ProductModelId == 2);
            Assert.IsTrue(item.RoutingsCount == 2);
            Assert.IsTrue(item.DurationDays == 5);
            Assert.IsTrue(item.ProductId == 3);
            Assert.IsTrue(item.OrderId == 4);
            Assert.IsTrue(item.ProductName == "PRODUCTNAAME");
            Assert.IsTrue(item.Locations.Count() == 2);
            Assert.IsTrue(item.Locations.ElementAt(0) == "LOCATION5");
            Assert.IsTrue(item.Locations.ElementAt(1) == "LOCATION6");
        }
    }
}
