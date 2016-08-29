using System;
using System.Collections.Generic;

namespace CurrencyExchange
{
    public interface IGraphWalker<T>
    {
        void DFS(IGraphNode<T> node, Dictionary<T, bool> visited, Action<IGraphNode<T>> action);
    }
}
