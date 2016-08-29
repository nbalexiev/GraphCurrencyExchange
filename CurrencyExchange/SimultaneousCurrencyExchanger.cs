namespace CurrencyExchange
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SimultaneousCurrencyExchanger : CurrencyExchangeStrategy
    {
        private static readonly double ExchangePercentage = 0.1;

        public void Exchange(Country country, List<Country> neighbors, MatrixWorld buffer)
        {
            Country cloneCountry = (Country)country.Clone();
            Exchange(cloneCountry, neighbors);

            buffer.SetElement(cloneCountry.Position.Row, cloneCountry.Position.Col, cloneCountry);
        }

        public void Exchange(Country country, World buffer)
        {
            int index = buffer.Nodes.FindIndex(e => e.Value.Currency == country.Currency);
            Country cloneCountry = buffer.Nodes[index].Value;

            Exchange(cloneCountry, country.Neighbors.OfType<Country>().ToList());
            buffer.Nodes[index] = cloneCountry;
        }

        private static void Exchange(Country country, List<Country> neighbors)
        {
            List<int> localCurrencies = new List<int>(country.Value.Currencies.Keys);
            foreach (int localCurrency in localCurrencies)
            {
                country.Currencies[localCurrency] = GetRemainderAfterExchange(country.Currencies[localCurrency], neighbors.Count);
            }

            foreach (Country neighbor in neighbors)
            {
                foreach (int currencyCode in neighbor.Currencies.Keys)
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

                    double amountFromNeighbor = GetRemainderAfterExchange(initialNeighbourAmount, neighbor.ExchangedWith.Count) * ExchangePercentage;

                    country.Currencies[currencyCode] = initialLocalAmount + amountFromNeighbor;
                    
                    //Console.Write("Exchange: {0}->{1}[{2}]; ", neighbour.Value.Name, c.Name, neighbourCurrency);
                }

                country.ExchangedWith.Add(neighbor);
                neighbor.ExchangedWith.Add(country);
            }

            //Console.WriteLine();
        }

        private static double GetRemainderAfterExchange(double inititialAmount, int exchngingCountriesCount)
        {
            //Exchanges happen one at a time, rather simulteneously.
            //Hence the remaider  for 10% exhchnage is: x - 0.1*x - 0.1(x-0.1*x)... which is (0.9^n)*x
            return inititialAmount * Math.Pow((1 - ExchangePercentage), exchngingCountriesCount);
        }

        public bool IsBuffered
        {
            get { return true; }
        }
    }
}
