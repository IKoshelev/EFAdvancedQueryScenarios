using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public class GridRequestSort
    {
        public string PropName { get; set; }
        public bool IsDescending { get; set; }
    }
    public class GridRequestFilter
    {
        public string PropName { get; set; }
        public string JsonValue { get; set; }
        public string Operand { get; set; }
    }

    public class GridRequest
    {
        public GridRequestSort[] Sort { get; set; } = new GridRequestSort[0];

        public GridRequestFilter[] Filter { get; set; } = new GridRequestFilter[0];

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public IQueryable<T> WrapQuery<T>(IQueryable<T> initialQuery)
        {
            var query = initialQuery;

            query = WrapFilter(query);

            query = WrapSort(query);

            query = WrapSkipTake(query);

            return query;
        }

        private IQueryable<T> WrapSkipTake<T>(IQueryable<T> initialQuery)
        {
            var query = initialQuery;

            if (Skip.HasValue)
            {
                query = query.Skip(Skip.Value);
            }

            if (Take.HasValue)
            {
                query = query.Take(Take.Value);
            }

            return query;
        }

        private IQueryable<T> WrapFilter<T>(IQueryable<T> initialQuery)
        {
            if (!Filter.Any())
            {
                return initialQuery;
            }

            var query = initialQuery;

            foreach (var filter in Filter)
            {
                query = WrapFilter(query, filter);
            }

            return query;
        }

        private static IQueryable<T> WrapFilter<T>(
          IQueryable<T> query,
          GridRequestFilter filter)
        {
            var type = typeof(T);
            var prop = type.GetProperty(filter.PropName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var parameter = Expression.Parameter(type, "par");

            var propAccessExpr = Expression.MakeMemberAccess(parameter, prop);
            object value = JsonConvert.DeserializeObject(filter.JsonValue, prop.PropertyType);
            Expression<Func<T, bool>> filterExpr;
    
            var comparison = GetComparator(filter.Operand, prop.PropertyType, propAccessExpr, Expression.Constant(value));

            if (IsNullable(prop.PropertyType))
            {
                var nullCheck = Expression.NotEqual(propAccessExpr, Expression.Constant(null));
                comparison = Expression.And(nullCheck,comparison);  
            }

            filterExpr = Expression.Lambda<Func<T, bool>>(comparison, new[] { parameter });

            var newQuery = query.Where(filterExpr);
            return newQuery;
        }

        private static Expression GetComparator(string operand, Type propType, Expression left, Expression right)
        {
            switch (operand)
            {
                case "Gt":
                    return Expression.GreaterThan(left, right);
                case "Eq":
                    return Expression.Equal(left, right);
                default :
                    // try to find a method by the name of operand
                    var MethodInfo = propType.GetMethod(operand, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    return Expression.Call(left, MethodInfo, right);
            }
        }

        private static bool IsNullable(Type type)
        {
            return type.IsClass
                    || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        private IQueryable<T> WrapSort<T>(IQueryable<T> initialQuery)
        {
            if (!Sort.Any())
            {
                return initialQuery;
            }

            var query = initialQuery;

            var isFirst = true;
            foreach (var sort in Sort)
            {
                query = WrapSort(query, sort, isFirst);
                isFirst = false;
            }

            return query;
        }

        private static IQueryable<T> WrapSort<T>(
            IQueryable<T> query,
            GridRequestSort sort,
            bool isFirst = false)
        {
            var propAccessExpr = GetPropAccesssLambdaExpr(typeof(T), sort.PropName);
            var orderMethodName = "";
            //var isInitialOrdering = query.Expression.Type == typeof(IOrderedQueryable<T>);
            if (isFirst)
            {
                orderMethodName = sort.IsDescending ? "OrderByDescending" : "OrderBy";
            } 
            else
            {
                orderMethodName = sort.IsDescending ? "ThenByDescending" : "ThenBy";
            }

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == orderMethodName && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propAccessExpr.ReturnType);
            var newQuery = (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, propAccessExpr });
            return newQuery;

            //var resultExpression = Expression.Call(
            //    typeof(Queryable), 
            //    orderMethodName, new Type[] { typeof(T), propAccessExpr.ReturnType },
            //                              query.Expression, Expression.Quote(propAccessExpr));
            //return query.Provider.CreateQuery<T>(resultExpression);
        }

        private static LambdaExpression GetPropAccesssLambdaExpr(Type type, string name)
        {
            var prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            var param = Expression.Parameter(type);
            var propAccess = Expression.Property(param, prop.Name);
            var expr = Expression.Lambda(propAccess, param);
            return expr;
        }
    }

    public class GridRequestWithAdditionalPayload<T> : GridRequest
    {
        public T Payload { get; set; }
    }

    public class TextSearchPayload
    {
        public string TextSearch { get; set; }
    }
}
