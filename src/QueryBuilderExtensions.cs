using System;
using System.Linq;
using ChainLinq.Visitors;

namespace ChainLinq {
    public static class QueryBuilderExtensions {
        public static QueryBuilder<T> Where<T>(this QueryBuilder<T> builder, ILinqNode visitor) => builder.Add(new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.Where)));
        public static QueryBuilder<T> Count<T>(this QueryBuilder<T> builder, Func<int> count) => builder.Add(new Projection<int>(count, typeof(Queryable),  nameof(Queryable.Count)));
    }
}