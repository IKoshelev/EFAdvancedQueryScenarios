using EFDataContext;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Repositories.TEst
{
    [TestClass]
    public class BusinessEntitesRepositoryTest
    {
        [TestMethod]
        public void GetBusinessEntityes_Test()
        {
            var fakeContext = new FakeAdventureWorksDataContext()
            {
                BusinessEntities = new FakeDbSet<BusinessEntity>()
                {
                    new BusinessEntity
                    {
                        BusinessEntityId = 2
                    }
                },
                People = new FakeDbSet<Person>()
                {
                    new Person()
                    {
                        BusinessEntityId = 2,
                        FirstName = "FIRSTNAME",
                        LastName = "LASTNAME"
                    }
                },
                BusinessEntityAddresses = new FakeDbSet<BusinessEntityAddress>()
                {
                    new BusinessEntityAddress()
                    {
                        BusinessEntityId = 2,
                        AddressId = 3
                    }
                },
                Addresses = new FakeDbSet<Address>()
                {
                    new Address()
                    {
                        AddressId = 3,
                        City = "CITY",
                        StateProvinceId = 4,
                    }
                },
                StateProvinces = new FakeDbSet<StateProvince>()
                {
                    new StateProvince()
                    {
                        StateProvinceId = 4,
                        Name = "PROVINCE"
                    }
                }
            };

           var repo = new BusinessEntityRepository(fakeContext);

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
