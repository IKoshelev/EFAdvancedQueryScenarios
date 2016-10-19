using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDataContext;

namespace Repositories.Test
{
    public class DataTestBase
    {
        protected FakeAdventureWorksDataContext Context()
        {
            return new FakeAdventureWorksDataContext();
        }

        protected FakeDbSet<T> Set<T>(params T[] items) 
            where T : class
        {
            var set = new FakeDbSet<T>();
            set.AddRange(items);
            return set;
        }
    }
}
