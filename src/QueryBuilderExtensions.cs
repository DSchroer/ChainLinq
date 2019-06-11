using ChainLinq.Visitors;

namespace ChainLinq {
    public static class QueryBuilderExtensions {
        public static QueryBuilder<T> Where<T>(this QueryBuilder<T> builder, ILinqNode visitor) => builder.Add(LinqMethods.Where(visitor));
    }
}