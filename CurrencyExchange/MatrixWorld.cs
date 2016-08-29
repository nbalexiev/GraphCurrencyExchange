namespace CurrencyExchange
{
    using System;
    using System.Collections.Generic;

    public class MatrixWorld : AbstractWorld<MatrixGraphWalker>
    {
        private Position2D firstElement;

        private MatrixWorld buffer;

        public MatrixWorld(int size)
        {
            this.Map = new Country[size,size];
            this.GraphWalker = new MatrixGraphWalker();
        }

        public MatrixWorld(Country[,] map)
        {
            this.Map = map;
            this.GraphWalker = new MatrixGraphWalker();

            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    if (this.Map[i, j] != null)
                    {
                        if (this.firstElement == null)
                        {
                            this.firstElement = new Position2D(i, j);
                        }

                        this.Map[i, j].Position = new Position2D(i, j);
                        this.Map[i, j].Graph = this;
                    }
                }
            }
        }

        //Represent the world map as a matrix.
        //This adds the limitation of each country having 8 neighbours at most.
        public Country[,] Map { get; private set; }

        public int Rows
        {
            get { return this.Map.GetLength(0); }
        }

        public int Cols
        {
            get { return this.Map.GetLength(1); }
        }

        public Country GetElement(int row, int col)
        {
            return this.Map[row, col];
        }

        public void SetElement(int row, int col, Country country)
        {
            this.Map[row, col] = country;

            if (country.Position == null)
            {
                country.Position = new Position2D(row, col);
            }

            if (country.Graph == null)
            {
                country.Graph = this;
            }

            if (this.firstElement == null)
            {
                this.firstElement = new Position2D(row, col);
            }
        }

        public override void AddNode(IGraphNode<Country> node)
        {
            throw new NotImplementedException();
        }

        public override void AddNode(Country value)
        {
            throw new NotImplementedException();
        }

        public override void AddEdge(IGraphNode<Country> from, IGraphNode<Country> to)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(Country value)
        {
            throw new NotImplementedException();
        }

        public override List<IGraphNode<Country>> Nodes
        {
            get
            {
                List<IGraphNode<Country>> result = new List<IGraphNode<Country>>();

                for (int i = 0; i < this.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < this.Map.GetLength(1); j++)
                    {
                        if (this.Map[i, j] != null)
                        {
                            result.Add(this.Map[i,j]);
                        }
                    }
                }

                return result;
            }
        }

        public override int Size
        {
            get
            {
                return this.CountryCount();
            }
        }

        public override void PrintWorld()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    Console.Write(this.Map[i, j] != null ? "1 " : "0 ");
                }
                Console.WriteLine();
            }
        }

        public override void PrintState()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    Country c = this.Map[i, j];
                    if (c != null)
                    {
                        Console.WriteLine("Country({0},{1}): {2}", i, j, c);
                    }
                }
            }

            Console.WriteLine("--------------------------------------------------");
        }

        public override void Exchange()
        {
            this.ClearExchangeData();
            
            if (this.CurrencyExchangeStrategy.IsBuffered && this.buffer == null)
            {
                this.buffer = new MatrixWorld(new Country[this.Rows, this.Cols]);
            }

            bool[,] visited = new bool[this.Rows, this.Cols];
            this.GraphWalker.DFS(this, this.firstElement, visited, (c, neighbors) => { this.CurrencyExchangeStrategy.Exchange(c, neighbors, null); });

            if(this.CurrencyExchangeStrategy.IsBuffered)
            {
                this.SwapWorlds(this, this.buffer);
            }
        }

        public override bool IsExchanged()
        {
            int countriesCount = this.Size;
            bool isExchanged = true;
            this.GraphWalker.DFS(
                this,
                this.firstElement,
                new bool[this.Rows, this.Cols],
                node => isExchanged &= node.Value.Currencies.Keys.Count == countriesCount);

            return isExchanged;
        }

        private void SwapWorlds(MatrixWorld world, MatrixWorld otherWorld)
        {
            Country[,] tmp = world.Map;
            world.Map = otherWorld.Map;
            otherWorld.Map = tmp;
        }

        public override int IslandCount()
        {
            bool[,] visited = new bool[this.Rows, this.Cols];

            int count = 0;
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    if (this.Map[i, j] != null && !visited[i, j])
                    {
                        this.GraphWalker.DFS(this, new Position2D(i, j), visited, (Action<Country>)null);
                        count++;
                    }
                }
            }

            return count;
        }

        public int CountryCount()
        {
            bool[,] visited = new bool[this.Rows, this.Cols];
            int count = 0;

            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    if (this.Map[i, j] != null && !visited[i, j])
                    {
                        this.GraphWalker.DFS(this,new Position2D(i, j), visited, c => { if (c != null) { count++; } });
                    }
                }
            }

            return count;
        }
    }
}
