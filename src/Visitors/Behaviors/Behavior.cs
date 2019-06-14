
using System;
using System.Linq.Expressions;
using ChainLinq.Core;

namespace ChainLinq.Visitors.Behaviors
{
    public abstract class Behavior : ILinqNode
    {
        internal ChainLinqProvider Provider { get; set; }

        public Behavior()
        {
        }

        public bool Create(ref Expression expression)
        {
            OnUnknownCreate(ref expression);
            return true;
        }

        public bool Execute(ref Expression expression)
        {
            OnUnknownExecute(ref expression);
            return true;
        }

        public abstract void OnUnknownCreate(ref Expression expression);
        public abstract void OnUnknownExecute(ref Expression expression);

        public void ClearNodes()
        {
            Provider.Visitors.Clear();
        }
    }
}