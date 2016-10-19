using Repositories.Test.Util;
using Repositories.Test.Util.MethodCallReplacement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Test.Util
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> FixDbFunctionCalls<T>(this IQueryable<T> source)
        {
            var replacer = new MethodCallReplacer();
            Expression newExpresiion = replacer.Visit(source.Expression);
            IQueryable<T> newQueryable = source.Provider.CreateQuery<T>(newExpresiion);

            return newQueryable;
        }
    }
}
