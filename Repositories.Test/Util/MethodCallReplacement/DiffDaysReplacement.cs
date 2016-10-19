using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Test.Util.MethodCallReplacement
{
    class DiffDaysReplacement : MethodReplacement
    {
        public DiffDaysReplacement()
        {
            MethodToReplace = typeof(DbFunctions).GetMethod(
                nameof(DbFunctions.DiffDays), new[] { typeof(DateTime?), typeof(DateTime?) });
        }

        public static int? DateDiffEquivalent(DateTime? dateLeft, DateTime? dateRight)
        {
            return dateLeft.HasValue && dateRight.HasValue
                    ? (int?)(dateRight.Value - dateLeft.Value).TotalDays
                    : (int?) null;
        } 

        public override Expression ReplaceCall(
            ExpressionVisitor visitor, MethodCallExpression nodeToReplace)
        {
            var argumentsOfExistingCall = nodeToReplace.Arguments;
            Expression dateArgLeftExpr = visitor.Visit(argumentsOfExistingCall[0]);
            Expression dateArgRightExpr = visitor.Visit(argumentsOfExistingCall[1]);
            MethodInfo replacementMethod = this.GetType().GetMethod(nameof(DateDiffEquivalent));
            MethodCallExpression replacementMethodCallExpr = Expression.Call(replacementMethod, dateArgLeftExpr, dateArgRightExpr);

            return replacementMethodCallExpr;
        }
    }
}
