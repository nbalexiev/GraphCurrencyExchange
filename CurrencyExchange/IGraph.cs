using System.Collections.Generic;

namespace CurrencyExchange
{
    using System.Security.Cryptography.X509Certificates;

    public interface IGraph<T>
    {
        void AddNode(IGraphNode<T> node);

        void AddNode(T value);

        void AddEdge(IGraphNode<T> from, IGraphNode<T> to);

        bool Contains(T value);

        List<IGraphNode<T>> Nodes { get; }

        int Size { get; }

        void PrintWorld();
    }
}
