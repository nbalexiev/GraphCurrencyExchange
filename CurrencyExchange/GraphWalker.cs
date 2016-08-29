using System;
using System.Collections.Generic;

namespace CurrencyExchange
{
    public class GraphWalker<T> : IGraphWalker<T>
    {
        public void DFS(IGraphNode<T> node, Dictionary<T, bool> visited, Action<IGraphNode<T>> action)
        {
            visited[node.Value] = true;

            foreach (IGraphNode<T> n in node.Neighbors)
            {
                if (this.ShouldVisit(n.Value, visited))
                {
                    this.DFS(n, visited, action);
                }
            }

            action?.Invoke(node);

        }

        private bool ShouldVisit(T index, Dictionary<T, bool> visited)
        {
            return !(visited.ContainsKey(index) && visited[index]);
        }
    }
}
