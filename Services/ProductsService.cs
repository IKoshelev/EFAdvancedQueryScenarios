using Repositories;
using Services.Model;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductsService
    {
        ProductGridRow[] GetProductsForGrid(GridRequestWithAdditionalPayload<TextSearchPayload> request);    
    }

    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productRepo;

        public ProductsService(IProductsRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public ProductGridRow[] GetProductsForGrid(GridRequestWithAdditionalPayload<TextSearchPayload> request)
        {
            var textSearch = request.Payload.TextSearch;
            var query = _productRepo.ReadGridProducts(textSearch);

            query = request.WrapQuery(query);

            return query.ToArray();
        }
    }
}
