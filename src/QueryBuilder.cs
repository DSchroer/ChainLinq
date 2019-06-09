using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ChainLinq.Core;
using ChainLinq.Visitors;

namespace ChainLinq {
    public class QueryBuilder {
        private readonly List<IExpressionVisitor> _visitors = new List<IExpressionVisitor>();

        public void Add(IExpressionVisitor visitor){
            _visitors.Add(visitor);
        }

        protected virtual IExpressionVisitor Fallback(Expression invocation){
            return new DisableUnknown(invocation);
        }

        public IQueryable<T> Build<T>(Expression<Func<IQueryable<T>>> rootQuery){
            var invocation = Expression.Invoke(rootQuery);
            Add(Fallback(invocation));
            return new Query<T>(new VisitorProvider(_visitors), invocation);
        }
    }
}