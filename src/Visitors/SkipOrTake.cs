

using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors
{
    public class SkipOrTake : Method
    {
        private readonly Action<int> _skipHandler;

        public SkipOrTake(Action<int> handler, Type memberType, string methodName) : base(memberType, methodName)
        {
            _skipHandler = handler;
        }

        public override bool VisitMethod(ref MethodCallExpression expression)
        {
            var argument = expression.Arguments[1] as ConstantExpression;
            if (argument.Value is int skipAmount)
            {
                _skipHandler(skipAmount);
                return true;
            }

            return false;
        }
    }
}