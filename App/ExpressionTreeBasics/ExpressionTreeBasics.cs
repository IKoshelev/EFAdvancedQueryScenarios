using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Basics
{
    public static class ExpressionTreeBasics
    {
        public static void Demo()
        {
            //var value = 55;

            //Ask the compiler to produce expression tree for us.
            Expression<Func<Bar, bool>> foo = (a) => a.Baz > 55;
            var result1 = foo.Compile().Invoke(new Bar() { Baz = 50 });
            var result2 = foo.Compile().Invoke(new Bar() { Baz = 60 });


            //Create the same expression tree manually
            var parameter = Expression.Parameter(typeof(Bar), "a");

            var body = Expression.GreaterThan(
                Expression.MakeMemberAccess(parameter, typeof(Bar).GetProperty(nameof(Bar.Baz))),
                Expression.Constant(55));

            Expression<Func<Bar, bool>> foo2 = Expression.Lambda<Func<Bar, bool>>(body, new[] { parameter });

            var result3 = foo2.Compile().Invoke(new Bar() { Baz = 50 });
            var result4 = foo2.Compile().Invoke(new Bar() { Baz = 60 });
        }
    }

    public class Bar
    {
        public int Baz { get; set; }
    }
}


