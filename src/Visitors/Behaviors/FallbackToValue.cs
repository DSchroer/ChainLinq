
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors.Behaviors
{
    public class FallbackToValue : Behavior
    {
        public Expression Value { get; }

        public FallbackToValue(Expression value)
        {
            Value = value;
        }

        protected override void OnUnknownCreate(ref Expression expression)
        {
            ClearNodes();
            expression = Value;
        }

        protected override void OnUnknownExecute(ref Expression expression)
        {
            ClearNodes();
            if(expression is MethodCallExpression call){
                var args = new List<Expression>(call.Arguments);
                args[0] = Value;
                expression = Expression.Call(call.Object, call.Method, args);
            }else{
                expression = Value;
            }
        }
    }
}