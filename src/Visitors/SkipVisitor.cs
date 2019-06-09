

using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors
{
    public class Skip : IExpressionVisitor
    {
        private readonly Action<int> _skipHandler;

        public Skip(Action<int> handler)
        {
            _skipHandler = handler;
        }

        public bool Visit(ref Expression expression, IExpressionHandler provider)
        {
            if (expression is MethodCallExpression node)
            {
                if (node.Method.DeclaringType == typeof(Queryable) && node.Method.Name == nameof(Queryable.Skip))
                {
                    var argument = node.Arguments[1] as ConstantExpression;
                    if(argument.Value is int skipAmount)
                    {
                        _skipHandler(skipAmount);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}