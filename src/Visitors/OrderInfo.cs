using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ChainLinq.Visitors
{
    public sealed class OrderInfo
    {
        public string PropertyName { get; }
        public OrderDirection Direction { get; }

        public OrderInfo(MemberExpression member, bool ascending)
        {
            PropertyName = PropName(member);
            Direction = ascending ? OrderDirection.Ascending : OrderDirection.Descending;
        }

        private string PropName(Expression expr){
            var strings = new List<string>();

            while(expr is MemberExpression member){
                strings.Add(member.Member.Name);
                expr = member.Expression;
            }

            strings.Reverse();
            return string.Join(".", strings);
        }
    }
}