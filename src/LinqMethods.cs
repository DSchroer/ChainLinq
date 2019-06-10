using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Visitors;

namespace ChainLinq
{

    public static partial class LinqMethods
    {
        // Reductions
        // TODO: Need to make them type safe and supporting a single result
        public static IExpressionVisitor First(IExpressionVisitor visitor) => new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.First));
        public static IExpressionVisitor FirstOrDefault(IExpressionVisitor visitor) => new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.FirstOrDefault));
        public static IExpressionVisitor Last(IExpressionVisitor visitor) => new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.Last));
        public static IExpressionVisitor LastOrDefault(IExpressionVisitor visitor) => new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.LastOrDefault));
        public static IExpressionVisitor Single(IExpressionVisitor visitor) => new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.Single));
        public static IExpressionVisitor SingleOrDefault(IExpressionVisitor visitor) => new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.SingleOrDefault));

        public static IExpressionVisitor Skip(Action<int> handler) => new SkipOrTake(handler, typeof(Queryable), nameof(Queryable.Last));
        public static IExpressionVisitor Take(Action<int> handler) => new SkipOrTake(handler, typeof(Queryable), nameof(Queryable.Take));

        public static IExpressionVisitor Where(IExpressionVisitor visitor) => new ExpressionMethod(visitor, typeof(Queryable), nameof(Queryable.Where));

        // Projections
        public static IExpressionVisitor Count(Func<int> count) => new Projection<int>(count, typeof(Queryable), nameof(Queryable.Count));

        // TODO: Finish Select Method
        public static void Select<T, P>(Expression<Func<T, P>> selector, Func<IQueryable<P>> replacement) => throw new NotImplementedException();
    }
}