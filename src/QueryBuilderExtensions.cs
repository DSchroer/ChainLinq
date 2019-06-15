using System;
using System.Linq;
using ChainLinq.Visitors;

namespace ChainLinq
{
    public static class QueryBuilderExtensions
    {
        /// <summary>
        /// Callback for where statements
        /// </summary>
        /// <param name="builder">The builder to use</param>
        /// <param name="handler">Skip callback</param>
        /// <typeparam name="T">Queryable type</typeparam>
        /// <returns>The builder</returns>
        public static QueryBuilder<T> Where<T>(this QueryBuilder<T> builder, ILinqNode visitor) => builder.Add(new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.Where)));

        /// <summary>
        /// Callback for skip with the amount
        /// </summary>
        /// <param name="builder">The builder to use</param>
        /// <param name="handler">Skip callback</param>
        /// <typeparam name="T">Queryable type</typeparam>
        /// <returns>The builder</returns>
        public static QueryBuilder<T> Skip<T>(this QueryBuilder<T> builder, Action<int> handler) => builder.Add(new SkipOrTake(handler, typeof(Queryable), nameof(Queryable.Skip)));

        /// <summary>
        /// Callback for take with the amount
        /// </summary>
        /// <param name="builder">The builder to use</param>
        /// <param name="handler">Take callback</param>
        /// <typeparam name="T">Queryable type</typeparam>
        /// <returns>The builder</returns>
        public static QueryBuilder<T> Take<T>(this QueryBuilder<T> builder, Action<int> handler) => builder.Add(new SkipOrTake(handler, typeof(Queryable), nameof(Queryable.Take)));

        /// <summary>
        /// Callback for ordering
        /// </summary>
        /// <param name="builder">The builder to use</param>
        /// <param name="handler">Ordering callback</param>
        /// <typeparam name="T">Queryable type</typeparam>
        /// <returns>The builder</returns>
        public static QueryBuilder<T> OrderBy<T>(this QueryBuilder<T> builder, Action<OrderInfo> handler)
        {
            return builder
                .Add(new OrderBy(handler, true, nameof(Queryable.OrderBy)))
                .Add(new OrderBy(handler, false, nameof(Queryable.OrderByDescending)));
        }

        /// <summary>
        /// Callback for sub ordering
        /// </summary>
        /// <param name="builder">The builder to use</param>
        /// <param name="handler">Ordering callback</param>
        /// <typeparam name="T">Queryable type</typeparam>
        /// <returns>The builder</returns>
        public static QueryBuilder<T> ThenBy<T>(this QueryBuilder<T> builder, Action<OrderInfo> handler)
        {
            return builder
                .Add(new OrderBy(handler, true, nameof(Queryable.ThenBy)))
                .Add(new OrderBy(handler, false, nameof(Queryable.ThenByDescending)));
        }

        // Projections
        /// <summary>
        /// Callback for count
        /// </summary>
        /// <param name="builder">The builder to use</param>
        /// <param name="count">Trigger to return the new value</param>
        /// <typeparam name="T">Queryable type</typeparam>
        /// <returns>The builder</returns>
        public static QueryBuilder<T> Count<T>(this QueryBuilder<T> builder, Func<int> count) => builder.Add(new Projection<int>(count, typeof(Queryable), nameof(Queryable.Count)));

    }
}