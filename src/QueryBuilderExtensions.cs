using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ChainLinq.Core;
using ChainLinq.Visitors;

namespace ChainLinq
{
    public static class QueryBuilderExtensions
    {
        public static IQueryable<T> Build<T>(this QueryBuilder builder, Expression<Func<IEnumerable<T>>> rootQuery)
        {
            //TODO: Simplify this call
            return builder.Build(() => rootQuery.Compile().Invoke().AsQueryable());
        }
    }
}