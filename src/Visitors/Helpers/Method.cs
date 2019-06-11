using System;
using System.Linq;
using System.Linq.Expressions;
using ChainLinq.Utils;

namespace ChainLinq.Visitors
{
    public abstract class Method : ILinqNode
    {
        private readonly Type _declaringType;
        private readonly string _methodName;

        public Method(Type declaringType, string methodName)
        {
            _declaringType = declaringType;
            _methodName = methodName;
        }

        public abstract bool VisitMethod(ref MethodCallExpression expression);

        public bool Create(ref Expression expression)
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

        public bool Execute(ref Expression expression)
        {
            return false;
        }
    }
}