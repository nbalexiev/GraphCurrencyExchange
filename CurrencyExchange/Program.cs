namespace CurrencyExchange
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            switch (args[0].ToLower())
            {
                case "matrixgraph":
                    MatrixWorld(GetSize(args));
                    break;
                case "graph":
                    World(GetSize(args));
                    break;
                default:
                    PrintHelp();
                    break;
            }
        }

        private static int GetSize(string[] args)
        {
            int size = -1;
            int.TryParse(args[1], out size);            

            return size;
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Syntax: {'matrixGraph' {size}|{'graph'} {country count}} size");
            Console.WriteLine("matrixGraph is using a matrix to represent a world. Every country is connect to its 8 neighbours in the matrix.");
            Console.WriteLine("graph is using a List of nodes to represent the world.");
            Console.WriteLine("Size and country count must be a positive integer");
        }

        private static void World(int countryCount)
        {
            if (countryCount < 0)
            {
                PrintHelp();
                return;
            }

            World world = WorldGenerator.GetWorld(500);
            world.PrintWorld();

            Console.WriteLine();
           
            Console.WriteLine(world.IterationsTillExchanged());

            Console.ReadLine();
        }

        private static void MatrixWorld(int size)
        {
            if (size < 0)
            {
                PrintHelp();
                return;
            }

            MatrixWorld world = WorldGenerator.GetMatrixWorld(size);
            world.PrintWorld();
            Console.WriteLine();
            Console.WriteLine(world.IterationsTillExchanged());

            Console.ReadLine();
        }
    }
}
