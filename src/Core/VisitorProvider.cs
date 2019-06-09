using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Visitors;

namespace ChainLinq.Core
{
    internal class VisitorProvider : QueryProvider, IExpressionHandler
    {
        private readonly IQueryProvider _fallback = new List<int>().AsQueryable().Provider;
        public List<IExpressionVisitor> Visitors { get; }

        public VisitorProvider(List<IExpressionVisitor> visitors)
        {
            Visitors = visitors;
        }

        public override object Execute(Expression expression)
        {
            return _fallback.Execute(expression);
        }

        protected override Expression Create(Expression expression)
        {
            foreach (var visitor in Visitors)
            {
                if (visitor.Visit(ref expression, this))
                {
                    break;
                }
            }

            return expression;
        }
    }
}
