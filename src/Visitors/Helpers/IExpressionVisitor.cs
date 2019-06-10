using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors{
    public interface IExpressionVisitor
    {
        bool Visit(ref Expression expression);
    }
}