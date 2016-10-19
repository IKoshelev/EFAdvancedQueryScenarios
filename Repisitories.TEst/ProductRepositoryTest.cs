using EFDataContext;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

            var result = repo.GetWorkOrderSummaries().ToArray();


        }
    }
}
