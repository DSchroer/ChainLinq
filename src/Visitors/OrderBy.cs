using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Utils;

namespace ChainLinq.Visitors
{
    internal class OrderBy : Method
    {
        private readonly Action<OrderInfo> _callback;
        private readonly bool _isAscending;

        public OrderBy(Action<OrderInfo> callback, bool isAscending, string methodName) : base(typeof(Queryable), methodName)
        {
            _callback = callback;
            _isAscending = isAscending;
        }

        public override bool VisitMethod(ref MethodCallExpression expression)
        {
            var argument = expression.Arguments[1];
            var unquoted = argument.StripQuotes();
            if (unquoted is LambdaExpression lambda && lambda.Body is MemberExpression member)
            {
                _callback(new OrderInfo(member, _isAscending));
                return true;
            }

            return false;
        }
    }
}