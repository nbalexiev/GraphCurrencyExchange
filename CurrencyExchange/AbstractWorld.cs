namespace CurrencyExchange
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public abstract class AbstractWorld<T> : ICloneable, IGraph<Country> where T : IGraphWalker<Country> 
    {
        public T GraphWalker { get; set; }

        public CurrencyExchangeStrategy CurrencyExchangeStrategy { get; set; }

        public abstract int Size { get; }

        public abstract List<IGraphNode<Country>> Nodes { get; }

        protected AbstractWorld()
        {
            this.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();
        }

        public abstract void AddNode(IGraphNode<Country> node);

        public abstract void AddNode(Country value);

        public abstract void AddEdge(IGraphNode<Country> from, IGraphNode<Country> to);

        public abstract bool Contains(Country value);

        public abstract void PrintWorld();

        public abstract void Exchange();

        public abstract int IslandCount();

        public abstract void PrintState();

        public bool CanExchange()
        {
            return this.IslandCount() == 1;
        }

        public virtual bool IsExchanged()
        {
            int countriesCount = this.Size;
            bool isExchanged = true;
            this.GraphWalker.DFS(
                this.Nodes.First(),
                new Dictionary<Country, bool>(),
                node => isExchanged &= node.Value.Currencies.Keys.Count == countriesCount);

            return isExchanged;
        }

        public int IterationsTillExchanged(bool printState = true, bool endlessLoop = false, int sleepInterval = 0)
        {
            int result = 0;
            if (printState)
            {
                this.PrintState();
            }
                
            while (!this.IsExchanged() || endlessLoop)
            {
                this.Exchange();
                if (printState)
                {
                    this.PrintState();
                    Thread.Sleep(sleepInterval);
                }
                result++;
            }

            return result;
        }

        protected void ClearExchangeData()
        {
            foreach (IGraphNode<Country> country in this.Nodes)
            {
                country.Value.ExchangedWith.Clear();
            }
        }

        public abstract object Clone();
    }
}
