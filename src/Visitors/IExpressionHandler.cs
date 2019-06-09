using System.Collections.Generic;

namespace ChainLinq.Visitors {
    public interface IExpressionHandler
    {
        List<IExpressionVisitor> Visitors {get;}
    }
}