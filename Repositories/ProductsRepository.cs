using EFDataContext;
using Shared.Dtos;
using System.Linq;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IProductsRepository
    {
        IQueryable<ProductGridRow> ReadGridProducts(string textSearch); 
    }

    public class ProductsRepository : EFRepositoryBase, IProductsRepository
    {
        public ProductsRepository(IAdventureWorksDataContext context)
            :base(context)
        {

        }

        public class ModelWithDescriptions
        {
            public ProductModel Model { get; set; }
            public IEnumerable<ProductDescription> Descriptions { get; set; }
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

        public IQueryable<ProductGridRow> ReadGridProducts(string textSearch)
        {
            bool needsTextSearch = string.IsNullOrWhiteSpace(textSearch) == false;

            Expression<Func<Product, bool>> textFilter = (p) => true;
            var descriptions = DataContext.ProductModels.Where(x => false).Select(x => new ModelWithDescriptions());
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
                            ModelName = model != null ? model.Name : ""
                        };

            return query;
        }
    }
}
