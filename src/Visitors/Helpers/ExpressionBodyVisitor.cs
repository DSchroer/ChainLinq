using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Utils;

namespace ChainLinq.Visitors{

    public abstract class ExpressionBodyVisitor : IExpressionVisitor
    {
        private readonly IExpressionVisitor _visitor;

        public bool Visit(ref Expression expression)
        {
            var unquoted = expression.StripQuotes();
            if(unquoted is LambdaExpression lambda){
                return Visit(lambda.Body);
            }

            return Visit(expression);
        }

        protected abstract bool Visit(Expression expression);
    }
}