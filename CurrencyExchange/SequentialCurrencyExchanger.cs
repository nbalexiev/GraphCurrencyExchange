namespace CurrencyExchange
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SequentialCurrencyExchanger : CurrencyExchangeStrategy
    {
        private static readonly double ExchangePercentage = 0.1;

        public void Exchange(Country country, List<Country> neighbors, MatrixWorld buffer)
        {
            Exchange(country, neighbors);
        }

        public void Exchange(Country country, World buffer)
        {
            Exchange(country, country.Neighbors.OfType<Country>().ToList());
        }

        private static void Exchange(Country country, List<Country> neighbors)
        {
            foreach (Country neighbor in neighbors)
            {
                if (country.ExchangedWith.Contains(neighbor))
                {
                    continue;
                }

                HashSet<int> currenciesToExchange = new HashSet<int>();
                currenciesToExchange.UnionWith(neighbor.Currencies.Keys);
                currenciesToExchange.UnionWith(country.Currencies.Keys);

                foreach (int currencyCode in currenciesToExchange)
                {
                    double initialLocalAmount;
                    if (!country.Currencies.TryGetValue(currencyCode, out initialLocalAmount))
                    {
                        initialLocalAmount = 0f;
                    }

                    double initialNeighbourAmount;
                    if (!neighbor.Currencies.TryGetValue(currencyCode, out initialNeighbourAmount))
                    {
                        initialNeighbourAmount = 0f;
                    }

                    double amountFromNeighbor = Math.Round(initialNeighbourAmount * ExchangePercentage, 2);
                    double amountToNeighbor = Math.Round(initialLocalAmount * ExchangePercentage, 2);

                    country.Currencies[currencyCode] = Math.Round(initialLocalAmount + amountFromNeighbor - amountToNeighbor, 2);
                    neighbor.Currencies[currencyCode] = Math.Round(initialNeighbourAmount - amountFromNeighbor + amountToNeighbor, 2);
                    
                    //Console.Write("Exchange: {0}->{1}[{2}]; ", neighbour.Value.Name, c.Name, neighbourCurrency);
                }

                country.ExchangedWith.Add(neighbor);
                neighbor.ExchangedWith.Add(country);
            }

            //Console.WriteLine();
        }

        public bool IsBuffered
        {
            get { return false; }
        }
    }
}
