using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Visitors;

namespace ChainLinq.Core
{
    internal class ChainLinqProvider : QueryProvider
    {
        private readonly IQueryProvider _fallback = new List<int>().AsQueryable().Provider;
        public List<ILinqNode> Visitors { get; } = new List<ILinqNode>();

        public override object Execute(Expression expression)
        {
            foreach (var visitor in Visitors)
            {
                if (visitor.Execute(ref expression))
                {
                    break;
                }
            }

            return _fallback.Execute(expression);
        }

        protected override Expression Create(Expression expression)
        {
            foreach (var visitor in Visitors)
            {
                if (visitor.Create(ref expression))
                {
                    break;
                }
            }

            return expression;
        }
    }
}
