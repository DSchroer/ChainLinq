
using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors {
    public class DisableUnknown : IExpressionVisitor
    {
        private readonly Expression _final;
        
        public DisableUnknown(Expression final){
            _final = final;
        }

        public bool Visit(ref Expression expression, IExpressionHandler provider)
        {
           expression = _final;
           provider.Visitors.Clear();
           return true;
        }
    }
}