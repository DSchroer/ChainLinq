using System;
using System.Collections.Generic;
using System.Linq;
using ChainLinq.Core;
using ChainLinq.Visitors;
using ChainLinq.Visitors.Behaviors;

namespace ChainLinq
{
    /// <summary>
    /// Build custom IQueryable<T>
    /// </summary>
    /// <typeparam name="T">The queryable type</typeparam>
    public class QueryBuilder<T>
    {
        private readonly List<ILinqNode> _visitors = new List<ILinqNode>();
        private readonly Behavior _behavior;

        public QueryBuilder() : this(new ThrowsException()) { }
        public QueryBuilder(Func<IEnumerable<T>> value) : this(new FallbackToValue<T>(value)) { }

        public QueryBuilder(Behavior behavior)
        {
            _behavior = behavior;
        }

        /// <summary>
        /// Add a new ILinqNode behavior for the IQueryable<T>
        /// </summary>
        /// <param name="visitor">The new visitor to add</param>
        /// <returns>The builder</returns>
        public QueryBuilder<T> Add(ILinqNode visitor)
        {
            _visitors.Add(visitor);
            return this;
        }

        /// <summary>
        /// Construct the IQueryable<T>
        /// </summary>
        /// <returns>A IQueryable with your behaviors</returns>
        public IQueryable<T> Build()
        {
            var visitor = new ChainLinqProvider();
            _behavior.Provider = visitor;

            visitor.Visitors.AddRange(_visitors);
            visitor.Visitors.Add(_behavior);

            return new Query<T>(visitor);
        }
    }
}