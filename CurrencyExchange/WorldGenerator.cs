namespace CurrencyExchange
{
    using System;

    public static class WorldGenerator
    {
        public static MatrixWorld GetMatrixWorld(int worldSize)
        {
            MatrixWorld world = new MatrixWorld(worldSize);

            Random rand = new Random();

            int probability = 25;
            int k = 0;
            do
            {
                for (int i = 0; i < worldSize; i++)
                {
                    for (int j = 0; j < worldSize; j++)
                    {
                        if (rand.Next(0, 100) > probability)
                        {
                            world.SetElement(j, i, new Country(k));
                            k++;
                        }
                    }
                }
                if (probability > 7)
                {
                    probability = probability / 2;
                }
            }
            while (!world.CanExchange());

            return world;
        }

        public static World GetWorld(int countryCount)
        {
            World world = new World();

            for (int i = 0; i < countryCount; i++)
            {
                world.AddNode(new Country(i));
            }

            Random rand = new Random();
            int probability = 25;
            do
            {
                for (int i = 0; i < countryCount - 1; i++)
                {
                    for (int j = i+1; j < countryCount; j++)
                    {
                        if (rand.Next(0,100) > probability)
                        {
                            if (world.Countries[i].Value.Neighbors.Count > 7 || world.Countries[j].Value.Neighbors.Count > 7)
                            {
                                continue;
                            }

                            if (!world.Nodes[i].Neighbors.Contains(world.Nodes[j]))
                            {
                                world.AddEdge(world.Countries[i], world.Countries[j]);
                            }                            
                        }
                    }
                }
                if (probability > 7)
                {
                    probability = probability / 2;
                }
            }
            while (!world.CanExchange());

            return world;
        }
    }
}
