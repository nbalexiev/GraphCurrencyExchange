using System.Collections.Generic;

namespace CurrencyExchange
{
    public interface IGraphNode<T>
    {
        T Value { get; }

        List<IGraphNode<T>> Neighbors { get; }
    }
}
