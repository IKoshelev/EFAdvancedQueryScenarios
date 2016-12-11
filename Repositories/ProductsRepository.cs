using EFDataContext;
using Shared.Dtos;
using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Repositories
{
    public interface IProductsRepository
    {
        IQueryable<ProductGridRow> ReadGridProducts(string textSearch);
        IQueryable<ModelWithDescriptions> GetProductModelsTextSearch(string textSearch);
        IQueryable<ProductModelOrderStatisticsDto> GetProductModelOrderStats();
        IQueryable<WorkOrderSummaryDto> GetWorkOrderSummaries();
        IQueryable<FreshProductWithBadReviewReportRow> GetFreshProductsWithBadReviews();
    }

    public class ModelWithDescriptions
    {
        public ProductModel Model { get; set; }
        public IEnumerable<ProductDescription> Descriptions { get; set; }
    }

    public class ProductsRepository : EFRepositoryBase, IProductsRepository
    {
        public ProductsRepository(IAdventureWorksDataContext context)
            :base(context)
        {

        }

        public IQueryable<ModelWithDescriptions> GetProductModelsTextSearch(string textSearch)
        {
            if (string.IsNullOrWhiteSpace(textSearch))
            {
                throw new ArgumentException();
            }

            var query = from model in DataContext.ProductModels
                        let descriptionLinks = DataContext
                                                .ProductModelProductDescriptionCultures
                                                .Where(x => x.ProductModelId == model.ProductModelId)
                                                .Select(x => x.ProductDescriptionId)

                        let descriptions = DataContext
                                                .ProductDescriptions
                                                .Where(x => descriptionLinks.Contains(x.ProductDescriptionId))
                                                .Where(x => x.Description.Contains(textSearch))

                        where model.Name.Contains(textSearch)
                                || descriptions.Any()

                        select new ModelWithDescriptions
                        {
                            Model = model,
                            Descriptions = descriptions
                        };

            return query;

        }
        
        public IQueryable<WorkOrderSummaryDto> GetWorkOrderSummaries()
        {
            var allDurationsAndRoutings = from model in DataContext.ProductModels
                         join product in DataContext.Products
                                on model.ProductModelId equals product.ProductModelId
                         join workOrder in DataContext.WorkOrders
                                on product.ProductId equals workOrder.ProductId
                         join workOrderRouting in DataContext.WorkOrderRoutings
                                on workOrder.WorkOrderId equals workOrderRouting.WorkOrderId
                                into routingsPerOrder

                         let duration = DbFunctions.DiffDays(workOrder.StartDate, workOrder.EndDate)

                         orderby duration descending

                         select new WorkOrderSummaryDto
                         {
                             ProductModelId = model.ProductModelId,
                             ModelName = model.Name,
                             ProductId = product.ProductId,
                             ProductName = product.Name,
                             OrderId = workOrder.WorkOrderId,
                             DurationDays = duration,
                             RoutingsCount = routingsPerOrder.Count(),
                             Locations = DataContext
                                                .Locations
                                                .Where(y => routingsPerOrder
                                                                .Select(z => z.LocationId)
                                                                .Contains(y.LocationId))
                                               .Select(z => z.Name)
                                               .Distinct()
                         };

            return allDurationsAndRoutings;
        }  

        public IQueryable<ProductModelOrderStatisticsDto> GetProductModelOrderStats()
        {
            var allDurationsAndRoutings = GetWorkOrderSummaries();

            var averagePerModel = allDurationsAndRoutings
                        .GroupBy(x => new { x.ProductModelId, x.ModelName })
                        .Select(x => new ProductModelOrderStatisticsDto
                        {
                            ModelId = x.Key.ProductModelId,
                            ModelName = x.Key.ModelName,
                            AverageDuration = x.Where(y => y.DurationDays.HasValue)
                                                .Average(y => y.DurationDays.Value),
                            AverageRoutings = x.Average(y => y.RoutingsCount)
                        });

            return averagePerModel;
        }

        public IQueryable<ProductGridRow> ReadGridProducts(string textSearch)
        {
            bool needsTextSearch = string.IsNullOrWhiteSpace(textSearch) == false;

            Expression<Func<Product, bool>> textFilter = (p) => true;
            var descriptions = DataContext
                .ProductModels.Where(x => false)
                .Select(x => new ModelWithDescriptions() { Model = x, Descriptions = x.ProductModelProductDescriptionCultures.Select(y => y.ProductDescription) });
            if (needsTextSearch)
            {
                textFilter = (p) => p.Name.Contains(textSearch)
                                    || p.ProductNumber.Contains(textSearch);

                descriptions = GetProductModelsTextSearch(textSearch);
            }

            var query = from product in DataContext.Products.AsExpandable()

                        let model = DataContext
                                        .ProductModels
                                        .Where(x => x.ProductModelId == product.ProductModelId)
                                        .FirstOrDefault()

                        let matchingDescriptions = descriptions.Where(x => x.Model.ProductModelId == product.ProductModelId)

                        where textFilter.Invoke(product)
                                || matchingDescriptions.Any()

                        select new ProductGridRow
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            ProductNumber = product.ProductNumber,
                            ModelName = model != null ? model.Name : "",
                            Descriptions = matchingDescriptions.SelectMany(x => x.Descriptions.Select(y => y.Description))
                        };

            return query;
        }

        public IQueryable<FreshProductWithBadReviewReportRow> GetFreshProductsWithBadReviews()
        {
            IQueryable<Product> freshProducts = GetFreshProducts();
            IQueryable<ProductModel> freshModels = GetFreshModels(DateTime.Today.AddYears(-5));
            IQueryable<ProductIdAverageReviewScore> averageReviewScores = GetProductAverageReviewScores();
            IQueryable<ProductReview> badRewies = GetBadReviews();
            IQueryable<ProductSubcategory> fullBikeSubcategories = GetFullBikeSubcategories();

            var query = from product in freshProducts
                        join model in freshModels
                            on product.ProductModelId equals model.ProductModelId

                        let averageReviewScore = averageReviewScores
                                                    .Where(x => x.ProductId == product.ProductId)
                                                    .Select(x => x.AverageReviewScore)
                                                    .FirstOrDefault()

                        let isAverageReviewScroePositive = averageReviewScore > 2.5

                        where badRewies.Select(r => r.ProductId).Contains(product.ProductId)

                        select new FreshProductWithBadReviewReportRow
                        {
                            ProductId = product.ProductId,
                            ProductName = product.Name,
                            ProductModel = model.CatalogDescription,
                            IsFullyAsembledBike = fullBikeSubcategories
                                                    .Any(sc => sc.ProductSubcategoryId == product.ProductSubcategoryId),
                            AverageReviewScore = averageReviewScore,
                            IsAverageReviewScroePositive = isAverageReviewScroePositive

                        };

            return query;
        }

        private IQueryable<Product> GetFreshProducts()
        {
            var freshnessCutoffDate = DateTime.Today.AddYears(-3);
            return DataContext.Products.Where(x => x.ModifiedDate > freshnessCutoffDate);
        }

        private IQueryable<ProductModel> GetFreshModels(DateTime modifiedAfter)
        {
            return DataContext.ProductModels.Where(x => x.ModifiedDate > modifiedAfter);
        }

        private IQueryable<ProductReview> GetBadReviews()
        {
            return DataContext.ProductReviews.Where(x => x.Rating <= 3);
        }

        private class ProductIdAverageReviewScore
        {
            public int ProductId { get; set; }
            public double AverageReviewScore { get; set; }
        }
        private IQueryable<ProductIdAverageReviewScore> GetProductAverageReviewScores()
        {
            return DataContext
                .ProductReviews
                .GroupBy(r => r.ProductId)
                .Select(rg => new ProductIdAverageReviewScore
                    {
                        ProductId = rg.Key,
                        AverageReviewScore = rg.Select(x => x.Rating).Average()
                    });
        }
      
        private IQueryable<ProductSubcategory> GetFullBikeSubcategories()
        {
            return DataContext.ProductSubcategories.Where(c => c.ProductCategoryId == 1);
        }
    }
}
