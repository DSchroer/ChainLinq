
using System;
using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors.Behaviors
{
    public class ThrowsException : Behavior
    {
        protected override void OnUnknownCreate(ref Expression expression)
        {
            throw new UnhandledNodeException(expression);
        }

        protected override void OnUnknownExecute(ref Expression expression)
        {
            throw new UnhandledNodeException(expression);
        }
    }
}