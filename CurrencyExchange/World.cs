using System;
using System.Collections.Generic;

namespace CurrencyExchange
{
    public class World : AbstractWorld<GraphWalker<Country>>
    {
        public World()
        {
            this.Countries = new List<IGraphNode<Country>>();
            this.GraphWalker = new GraphWalker<Country>();
        }

        public World(List<IGraphNode<Country>> nodes)
        {
            this.Countries = new List<IGraphNode<Country>>(nodes);
            this.GraphWalker = new GraphWalker<Country>();
        }

        public List<IGraphNode<Country>> Countries { get; set; }

        public override List<IGraphNode<Country>> Nodes
        {
            get
            {
                return this.Countries;
            }
        }

        public override int Size
        {
            get
            {
                return this.Nodes.Count;
            }
        }

        public override void AddNode(IGraphNode<Country> node)
        {
            this.Countries.Add(node.Value);
        }

        public override void AddNode(Country value)
        {
            this.Countries.Add(value);
        }

        public override void AddEdge(IGraphNode<Country> from, IGraphNode<Country> to)
        {
            from.Neighbors.Add(to);
            to.Neighbors.Add(from);
        }

        public override bool Contains(Country country)
        {
            return this.Countries.Contains(country);
        }

        public override void PrintWorld()
        {
            foreach (IGraphNode<Country> graphNode in Nodes)
            {
                Console.Write("{0}: ", graphNode.Value.Name);
                foreach (IGraphNode<Country> neighbor in graphNode.Neighbors)
                {
                    Console.Write("{0}->{1};", graphNode.Value.Name, neighbor.Value.Name);
                }
                Console.WriteLine();
            }
        }

        public override void PrintState()
        {
            this.GraphWalker.DFS(this.Nodes[0], new Dictionary<Country, bool>(), c => Console.WriteLine(
                $"Country({c.Value.Currency}): {c}"));
            Console.WriteLine("--------------------------------------------------");
        }

        public override void Exchange()
        {
            this.ClearExchangeData();

            World buffer = this.CurrencyExchangeStrategy.IsBuffered ? (World)this.Clone() : null;

            Dictionary<Country, bool> visited = new Dictionary<Country, bool>();
            this.GraphWalker.DFS(this.Nodes[0], visited, e => { this.CurrencyExchangeStrategy.Exchange(e.Value, buffer); });

            if (this.CurrencyExchangeStrategy.IsBuffered)
            {
                this.SwapWorlds(this, buffer);
            }
        }

        private void SwapWorlds(World world, World otherWorld)
        {
            List<IGraphNode<Country>> tmp = world.Countries;
            world.Countries = otherWorld.Countries;
            otherWorld.Countries = tmp;
        }

        public override int IslandCount()
        {
            Dictionary<Country, bool> visited = new Dictionary<Country, bool>();

            int count = 0;
            foreach (IGraphNode<Country> c in this.Nodes)
            {
                if (!(visited.ContainsKey(c.Value) && visited[c.Value]))
                {
                    this.GraphWalker.DFS(c, visited, null);
                    count++;
                }
            }

            return count;
        }

        public override object Clone()
        {
            World clone = new World();
            clone.GraphWalker = this.GraphWalker;
            foreach(IGraphNode<Country> c in this.Countries)
            {
                clone.AddNode((Country)((Country)c).Clone());
            }

            foreach (IGraphNode<Country> c in clone.Countries)
            {
                List<IGraphNode<Country>> tmp = new List<IGraphNode<Country>>(c.Neighbors);
                foreach (IGraphNode<Country> neighbor in tmp)
                {
                    c.Neighbors.Remove(neighbor);
                    int index = this.Countries.IndexOf(neighbor);
                    c.Neighbors.Add(clone.Countries[index]);
                }
            }

            return clone;
        }
    }
}
