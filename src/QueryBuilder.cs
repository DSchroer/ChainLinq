using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ChainLinq.Core;
using ChainLinq.Visitors;
using ChainLinq.Visitors.Behaviors;

namespace ChainLinq
{
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

        public QueryBuilder<T> Add(ILinqNode visitor)
        {
            _visitors.Add(visitor);
            return this;
        }

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