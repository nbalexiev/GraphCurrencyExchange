namespace CurrencyExchange
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Country : ICloneable, IMatrixGraphNode<Country>
    {
        public string Name { get; set; }

        public int Currency { get; set; }

        public Position2D Position { get; set; }

        public MatrixWorld Graph { get; set; }

        public Dictionary<int, double> Currencies { get; set; }

        public List<Country> ExchangedWith = new List<Country>();

        public Country(int currency)
        {
            this.Currency = currency;
            this.Name = CurrencyGenerator.GetCurrencySymbol(currency);
            this.Currencies = new Dictionary<int, double> { { currency, 100 } };
            this.Neighbors = new List<IGraphNode<Country>>();
        }

        public Country Value
        {
            get
            {
                return this;
            }
        }

        public List<IGraphNode<Country>> Neighbors
        {
            get;
            set;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<int, double> kv in this.Currencies.OrderBy(kvp => kvp.Key))
            {
                sb.Append(string.Format("[{0}:{1}],", CurrencyGenerator.GetCurrencySymbol(kv.Key), kv.Value));
            }

            return sb.ToString().TrimEnd(',');
        }

        public object Clone()
        {
            Country result = new Country(this.Currency)
                                 {
                                     Name = this.Name,
                                     Currencies = new Dictionary<int, double>(this.Currencies),
                                     Neighbors = new List<IGraphNode<Country>>(this.Neighbors),
                                     Position = (Position2D)this.Position?.Clone(),
                                     Graph = this.Graph
                                 };

            return result;
        }
    }
}
