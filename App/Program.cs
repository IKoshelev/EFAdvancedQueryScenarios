using System.Linq;

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
        }
    }
}
