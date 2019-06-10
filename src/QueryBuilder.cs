using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ChainLinq.Core;
using ChainLinq.Visitors;

namespace ChainLinq
{
    public class QueryBuilder<T>
    {
        private readonly List<IExpressionVisitor> _visitors = new List<IExpressionVisitor>();
        private Fallback _fallback;

        public QueryBuilder<T> Add(IExpressionVisitor visitor)
        {
            _visitors.Add(visitor);
            return this;
        }

        public QueryBuilder<T> Fallback(Func<IQueryable<T>> value)
        {
            _fallback = new Fallback(CallableFunc(value));
            return this;
        }

        public QueryBuilder<T> Fallback(Func<IEnumerable<T>> value)
        {
            return Fallback(() => value().AsQueryable());
        }

        public IQueryable<T> Build()
        {
            var visitor = new VisitorProvider();

            visitor.Visitors.AddRange(_visitors);

            if (_fallback != null)
            {
                _fallback.Provider = visitor;
                visitor.Visitors.Add(_fallback);
            }

            return new Query<T>(visitor, CallableFunc(() => throw new InvalidOperationException()));
        }

        private Expression CallableFunc(Func<IQueryable<T>> core)
        {
            var methodInfo = typeof(Func<IQueryable<T>>).GetMethod(nameof(Func<IQueryable<T>>.Invoke));
            return Expression.Call(Expression.Constant(core), methodInfo);
        }
    }
}