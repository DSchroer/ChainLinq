using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Utils;

namespace ChainLinq.Visitors
{
    public class ExpressionMethod : Method
    {
        private readonly IExpressionVisitor _visitor;

        public ExpressionMethod(IExpressionVisitor visitor, Type memberType, string methodName) : base(memberType, methodName)
        {
            _visitor = visitor;
        }

        public override bool VisitMethod(ref MethodCallExpression expression)
        {
            var argument = expression.Arguments[1];
            return _visitor.Visit(ref argument);
        }
    }
}