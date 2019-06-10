using System;
using System.Linq.Expressions;

namespace ChainLinq.Visitors {
    public class Projection<T> : Terminator
    {
        private readonly Func<T> _projection;

        public Projection(Func<T> projection, Type memberType, string methodName): base(memberType, methodName){
            _projection = projection;
        }

        public override bool VisitMethod(ref MethodCallExpression expression)
        {
            var methodInfo = typeof(Func<T>).GetMethod(nameof(Func<T>.Invoke));
            var invocation = Expression.Call(Expression.Constant(_projection), methodInfo);

            expression = invocation;
            return true;
        }
    }
}