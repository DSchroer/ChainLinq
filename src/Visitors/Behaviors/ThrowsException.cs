
using System.Linq.Expressions;

namespace ChainLinq.Visitors.Behaviors
{
    internal class ThrowsException : Behavior
    {
        public override void OnUnknownCreate(ref Expression expression)
        {
            throw new UnhandledNodeException(expression);
        }

        public override void OnUnknownExecute(ref Expression expression)
        {
            throw new UnhandledNodeException(expression);
        }
    }
}