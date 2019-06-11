
using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors
{
    internal class Fallback : ILinqNode
    {
        public Expression Value { get; }
        public ChainLinqProvider Provider { get; set; }

        public Fallback(Expression value)
        {
            Value = value;
        }

        public bool Create(ref Expression expression)
        {
            Provider.Visitors.Clear();
            expression = Value;
            return true;
        }

        public bool Execute(ref Expression expression)
        {
            Provider.Visitors.Clear();
            expression = Value;
            return true;
        }
    }
}