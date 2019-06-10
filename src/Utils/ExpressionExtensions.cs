using System.Linq.Expressions;

namespace ChainLinq.Utils
{
    public static class ExpressionExtensions
    {
        public static Expression StripQuotes(this Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }

            return e;

        }
    }
}