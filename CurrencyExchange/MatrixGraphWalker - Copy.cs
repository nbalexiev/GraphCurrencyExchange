using System;
using System.Collections.Generic;

namespace CurrencyExchange
{
    using System.Linq;

    public class MatrixGraphWalker : IGraphWalker<Country>
    {
        private static readonly int[] RowNumberIterator = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private static readonly int[] ColNumberIterator = { -1, 0, 1, -1, 1, -1, 0, 1 };

        public void DFS(MatrixWorld graph, Position2D pos, bool[,] visited, Action<Country> action)
        {
            this.DFSinStack(graph, pos, visited, action, null);
        }

        public void DFS(MatrixWorld graph, Position2D pos, bool[,] visited, Action<Country, List<Country>> action)
        {
            this.DFSinStack(graph, pos, visited, null, action);
        }

        private void DFS(MatrixWorld graph, Position2D pos, bool[,] visited, Action<Country> nodeAction, Action<Country, List<Country>> neigbourAction)
        {
            nodeAction?.Invoke(graph.GetElement(pos.Row, pos.Col));
            visited[pos.Row, pos.Col] = true;

            List<Country> neighbours = new List<Country>();
            if (this.Exists(graph, pos))
            {
                for (int k = 0; k < 8; k++)
                {
                    Position2D nextPosition = new Position2D(
                        pos.Row + RowNumberIterator[k],
                        pos.Col + ColNumberIterator[k]);

                    if (this.ShouldVisit(graph, nextPosition, visited))
                    {
                        this.DFS(graph, nextPosition, visited, nodeAction, neigbourAction);
                    }

                    if (this.Exists(graph, nextPosition))
                    {
                        neighbours.Add(graph.GetElement(pos.Row + RowNumberIterator[k], pos.Col + ColNumberIterator[k]));
                    }
                }

                neigbourAction?.Invoke(graph.GetElement(pos.Row, pos.Col), neighbours);
            }
        }

        private void DFSinStack(MatrixWorld graph, Position2D startPosition, bool[,] visited, Action<Country> nodeAction, Action<Country, List<Country>> neigbourAction)
        {
            Stack<Position2D> stack = new Stack<Position2D>();
            stack.Push(startPosition);
            while(stack.Count != 0)
            {
                Position2D position = stack.Pop();
                if (visited[position.Row, position.Col])
                {
                    continue;
                }

                nodeAction?.Invoke(graph.GetElement(position.Row, position.Col));
                visited[position.Row, position.Col] = true;
                List<Country> neighbours = new List<Country>();
                for (int k = 0; k < 8; k++)
                {
                    Position2D nextPosition = new Position2D(
                        position.Row + RowNumberIterator[k],
                        position.Col + ColNumberIterator[k]);

                    if (this.ShouldVisit(graph, nextPosition, visited))
                    {
                        stack.Push(nextPosition);
                    }

                    if (this.Exists(graph, nextPosition))
                    {
                        neighbours.Add(graph.GetElement(nextPosition.Row, nextPosition.Col));
                    }
                }

                neigbourAction?.Invoke(graph.GetElement(position.Row, position.Col), neighbours);
            }
        }

        private bool ShouldVisit(MatrixWorld graph, Position2D position, bool[,] visited)
        {
            return (this.Exists(graph, position) && !visited[position.Row, position.Col]);
        }

        private bool Exists(MatrixWorld graph, Position2D position)
        {
            return (position.Row >= 0) && (position.Row < graph.Rows) &&
                   (position.Col >= 0) && (position.Col < graph.Cols) &&
                   (graph.GetElement(position.Row, position.Col) != null);
        }

        public void DFS(IGraphNode<Country> node, Dictionary<Country, bool> visited, Action<IGraphNode<Country>> action)
        {
            bool[,] visitedArr = ToMatrix(visited, node.Value.Graph);
            this.DFS(node.Value.Graph, node.Value.Position, visitedArr, action);
            ToDictionary(node.Value.Graph, visitedArr, visited);
        }

        private static bool[,] ToMatrix(Dictionary<Country, bool> visited, MatrixWorld graph)
        {            
            bool[,] result = new bool[graph.Cols, graph.Rows];
            foreach (Country country in visited.Keys)
            {
                result[country.Position.Row, country.Position.Col] = visited[country];
            }

            return result;
        }

        private static void ToDictionary(MatrixWorld graph, bool[,] visited, Dictionary<Country, bool> visitedDict)
        {
            for (int i = 0; i < graph.Rows; i++)
            {
                for (int j = 0; j < graph.Cols; j++)
                {
                    if (graph.Map[i, j] != null)
                    {
                        visitedDict[graph.Map[i, j]] = visited[i, j];
                    }
                }
            }
        }
    }
}
