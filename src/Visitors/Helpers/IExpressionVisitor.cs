using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors{
    public interface IExpressionVisitor
    {
        bool Execute(ref Expression expression);
        bool Create(ref Expression expression);
    }
}