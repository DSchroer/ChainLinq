using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Utils;

namespace ChainLinq.Visitors
{
    public abstract class Terminator : ILinqNode
    {
        private readonly Type _declaringType;
        private readonly string _methodName;

        public Terminator(Type declaringType, string methodName)
        {
            _declaringType = declaringType;
            _methodName = methodName;
        }

        public abstract bool VisitMethod(ref MethodCallExpression expression);

        public bool Create(ref Expression expression)
        {
            return false;
        }

        public bool Execute(ref Expression expression)
        {
             if (expression is MethodCallExpression node)
            {
                if (node.Method.DeclaringType == _declaringType && node.Method.Name == _methodName)
                {
                    if(VisitMethod(ref node)){
                        expression = node;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}