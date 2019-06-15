using System;
using System.Linq.Expressions;

namespace ChainLinq
{
    /// <summary>
    /// Exception indicating that a Linq node with no behavior defined was added.
    /// </summary>
    public class UnhandledNodeException : Exception
    {
        /// <summary>
        /// The expression that was added.
        /// </summary>
        /// <value>The expression</value>
        public Expression Expression { get; }

        public UnhandledNodeException(Expression expression) : base("An unhandled query node was encountered")
        {
            Expression = expression;
        }
    }
}