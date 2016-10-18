using EFDataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;

namespace Repositories.Subqueries
{
    internal static class AddressSubqueries
    {
        internal static Expression<Func<string, string, string>> FormatCityAndProvince =
            (city, province) => "The glorious city of " + city + " of the wonderfull province of " + province;

        internal static Expression<Func<IAdventureWorksDataContext, int, string>> GetStandardAddressDescription =
            (DataContext, addressId) => DataContext
                                            .Addresses
                                            .AsExpandable()
                                            .Where(x => x.AddressId == addressId)
                                            .Join(
                                                DataContext.StateProvinces,
                                                adr => adr.StateProvinceId,
                                                prov => prov.StateProvinceId,
                                                (adr, prov) => FormatCityAndProvince.Invoke(adr.City, prov.Name))
                                            .FirstOrDefault();

        internal static Expression<Func<IAdventureWorksDataContext, int, IEnumerable<string>>> GetAddressesByBusinessEntityId =
            (DataContext, businessEntityId) => DataContext
                                            .BusinessEntityAddresses
                                            .Where(bae => bae.BusinessEntityId == businessEntityId)
                                            .Select(bae => AddressSubqueries.GetStandardAddressDescription.Invoke(DataContext, bae.AddressId));
    }
}
