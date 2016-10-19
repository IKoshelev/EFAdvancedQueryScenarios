using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Test.Util.MethodCallReplacement
{
    public class MethodCallReplacer : ExpressionVisitor
    {
        private MethodReplacement[] AvailableReplacements { get; set; } =
            new MethodReplacement[]
            {
                new DateDiffReplacement()
            };

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var methodReplacement = AvailableReplacements
                .FirstOrDefault(x => node.Method == x.MethodToReplace);

            if(methodReplacement == null)
            {
                return base.VisitMethodCall(node);
            }

            var newMethodCall = methodReplacement.ReplaceCall(this, node);

            return newMethodCall;
        }


    }

    public abstract class MethodReplacement
    {
        public MethodInfo MethodToReplace { get; protected set; }

        public abstract Expression ReplaceCall(
            ExpressionVisitor visitor, MethodCallExpression callToReplace);
    }
}
