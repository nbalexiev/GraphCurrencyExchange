using System.Collections.Generic;

namespace CurrencyExchange
{
    public interface CurrencyExchangeStrategy
    {
        void Exchange(Country country, List<Country> neighbors, MatrixWorld buffer);

        void Exchange(Country country, World buffer);

        bool IsBuffered { get; }
    }
}
