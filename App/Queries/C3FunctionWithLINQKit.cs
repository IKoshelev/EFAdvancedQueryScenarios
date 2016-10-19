using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Queries
{
    public static class C3FunctionWithLINQKit
    {
        public static void Query1(IBusinessEntityRepository repo)
        {
            var result1 = repo.GetBusinessEntities().ToArray();


    }
}
