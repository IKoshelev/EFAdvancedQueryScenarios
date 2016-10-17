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

        // GET api/values
        public ProductGridRow[] Get([FromUri]string query)
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

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
