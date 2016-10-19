using EFDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos;
using LinqKit;
using Repositories.Subqueries;

namespace Repositories
{
    public interface IBusinessEntityRepository
    {
        IQueryable<BusinessEntityWithContactsDto> GetBusinessEntities();

    }

    public class BusinessEntityRepository : EFRepositoryBase, IBusinessEntityRepository
    {
        public BusinessEntityRepository(IAdventureWorksDataContext context)
            :base(context)
        {

        }

        public IQueryable<BusinessEntityWithContactsDto> GetBusinessEntities()
        {
            var query = DataContext.BusinessEntities.AsExpandable()
                .Select(x => new BusinessEntityWithContactsDto
                {
                    BusinessEntityId = x.BusinessEntityId,
                    ContactName = DataContext
                            .People
                            .Where(p => p.BusinessEntityId == x.BusinessEntityId)
                            .Select(p => p.FirstName + " " + p.LastName)
                            .FirstOrDefault(),

                    Addresses = AddressSubqueries.GetAddressesByBusinessEntityId.Invoke(DataContext, x.BusinessEntityId)
                });

            return query;
        }
    }
}
