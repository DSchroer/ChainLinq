using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors{
    public interface ILinqNode
    {
        bool Execute(ref Expression expression);
        bool Create(ref Expression expression);
    }
}