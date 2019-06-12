using System;
using System.Linq.Expressions;

namespace ChainLinq
{
    public class UnhandledNodeException : Exception
    {
        public Expression Expression { get; }

        public UnhandledNodeException(Expression expression) : base("An unhandled query node was encountered")
        {
            Expression = expression;
        }
    }
}