using EFDataContext;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Repositories.Test
{
    [TestClass]
    public class BusinessEntitesRepositoryTest:  DataTestBase
    {
        [TestMethod]
        public void GetBusinessEntityes_Test()
        {
            var context = Context();
            context.BusinessEntities = Set(new BusinessEntity
            {
                BusinessEntityId = 2
            });

            context.People = Set(new Person()
            {
                BusinessEntityId = 2,
                FirstName = "FIRSTNAME",
                LastName = "LASTNAME"
            });

            context.BusinessEntityAddresses = Set(new BusinessEntityAddress()
            {
                BusinessEntityId = 2,
                AddressId = 3
            });

            context.Addresses = Set(new Address()
            {
                AddressId = 3,
                City = "CITY",
                StateProvinceId = 4,
            });

            context.StateProvinces = Set(new StateProvince()
            {
                StateProvinceId = 4,
                Name = "PROVINCE"
            });
                            
           var repo = new BusinessEntityRepository(context);

           var result = repo.GetBusinessEntityes().ToArray();

            Assert.IsTrue(result.Count() == 1);
            var ent = result[0];
            Assert.IsTrue(ent.BusinessEntityId == 2);
            Assert.IsTrue(ent.ContactName == "FIRSTNAME LASTNAME");
            Assert.IsTrue(ent.Addresses.Count() == 1);
            var adr = ent.Addresses.ElementAt(0);
            Assert.IsTrue(adr == "The glorious city of CITY of the wonderfull province of PROVINCE");
        }
    }
}
