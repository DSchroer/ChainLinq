using System;
using System.Linq;
using System.Linq.Expressions;

namespace ChainLinq.Visitors
{

    public class Equals<TRoot, TValue> : ExpressionBodyVisitor
    {
        private readonly Action<TValue> _callback;

        public Equals(Expression<Func<TRoot, TValue>> accessor, Action<TValue> callback)
        {
            _callback = callback;
        }

        protected override bool Visit(Expression expression)
        {
            if (expression is BinaryExpression binary && binary.NodeType == ExpressionType.Equal)
            {
                var left = binary.Left;
                var right = binary.Right;

                if (left is MemberExpression && right is ConstantExpression)
                {
                   return CheckEqual((MemberExpression)left, (ConstantExpression)right);
                }

                if (right is MemberExpression && left is ConstantExpression)
                {
                   return CheckEqual((MemberExpression)right, (ConstantExpression)left);
                }

            }
            return false;
        }

        private bool CheckEqual(MemberExpression member, ConstantExpression constant)
        {
            if (constant.Type == typeof(TValue))
            {
                _callback((TValue)constant.Value);
                return true;
            }

            return false;
        }
    }
}