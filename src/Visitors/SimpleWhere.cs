using System;
using System.Linq.Expressions;

namespace ChainLinq.Visitors{
    public class SimpleWhere<TType, TValue> : IExpressionVisitor
    {
        public SimpleWhere(Expression<Func<TType, TValue>> accessor, Action<TValue> callback){

        }

        public bool Visit(ref Expression expression, IExpressionHandler provider)
        {
            throw new System.NotImplementedException();
        }
    }
}