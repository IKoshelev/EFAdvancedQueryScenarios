using System.Collections.Generic;
using System.Web.Http;
using Services.Model;

using Services;
using Shared.Dtos;

namespace FrontEnd.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IProductsService _productsService;

        public ValuesController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public ProductGridRow[] GetProducts([FromUri]string query = null)
        {
            var req = new GridRequestWithAdditionalPayload<TextSearchPayload>()
            {
                Payload = new TextSearchPayload()
                {
                    TextSearch = query
                }
            };
            return _productsService.GetProductsForGrid(req);
        }
    }
}
