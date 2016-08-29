namespace CurrencyExchangeTests
{
    using System;
    using CurrencyExchange;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WorldTests
    {
        [TestMethod]
        public void TestExchangeIterations_TwoCountries()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);

            World world = new World();
            world.CurrencyExchangeStrategy = new SequentialCurrencyExchanger();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddEdge(c1, c2);

            Assert.AreEqual(1, world.IterationsTillExchanged());
        }

        [TestMethod]
        public void TestExchangeIterations_FiveCountries()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);
            Country c3 = new Country(2);
            Country c4 = new Country(3);
            Country c5 = new Country(4);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddNode(c4);
            world.AddNode(c5);
            world.AddEdge(c1, c3);
            world.AddEdge(c2, c4);
            world.AddEdge(c3, c4);
            world.AddEdge(c4, c5);

            Assert.AreEqual(3, world.IterationsTillExchanged());
        }

        [TestMethod]
        public void TestExchangeIterations_FiveCountries_Simulteneous()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);
            Country c3 = new Country(2);
            Country c4 = new Country(3);
            Country c5 = new Country(4);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddNode(c4);
            world.AddNode(c5);
            world.AddEdge(c1, c3);
            world.AddEdge(c2, c4);
            world.AddEdge(c3, c4);
            world.AddEdge(c4, c5);
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();

            Assert.AreEqual(3, world.IterationsTillExchanged());
        }


        [TestMethod]
        public void TestExchange_TwoCountries()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);

            World world = new World();
            world.CurrencyExchangeStrategy = new SequentialCurrencyExchanger();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddEdge(c1, c2);
            world.PrintState();

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(90, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(10, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(90, world.Nodes[1].Value.Currencies[1]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(82, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(18, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(18, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(82, world.Nodes[1].Value.Currencies[1]);
        }

        [TestMethod]
        public void TestExchange_TwoCountries_Simultaneous()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);

            World world = new World();
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddEdge(c1, c2);
            world.PrintState();

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(90, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(10, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(90, world.Nodes[1].Value.Currencies[1]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(82, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(18, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(18, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(82, world.Nodes[1].Value.Currencies[1]);
        }

        [TestMethod]
        public void TestExchange_ThreeCountries()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);
            Country c3 = new Country(2);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddEdge(c1, c2);
            world.AddEdge(c2, c3);
            world.PrintState();

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(90, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(9, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(1, world.Nodes[0].Value.Currencies[2]);

            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(81, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(9, world.Nodes[1].Value.Currencies[2]);

            Assert.IsFalse(world.Nodes[2].Value.Currencies.ContainsKey(0));
            Assert.AreEqual(90, world.Nodes[2].Value.Currencies[2]);
            Assert.AreEqual(10, world.Nodes[2].Value.Currencies[1]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(81.9, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(15.49, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(2.61, world.Nodes[0].Value.Currencies[2]);

            Assert.AreEqual(17.1, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(67.41, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(15.49, world.Nodes[1].Value.Currencies[2]);

            Assert.AreEqual(1, world.Nodes[2].Value.Currencies[0]);
            Assert.AreEqual(17.1, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(81.9, world.Nodes[2].Value.Currencies[2]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(75.26, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(20.18, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(4.56, world.Nodes[0].Value.Currencies[2]);

            Assert.AreEqual(22.13, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(57.69, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(20.18, world.Nodes[1].Value.Currencies[2]);

            Assert.AreEqual(2.61, world.Nodes[2].Value.Currencies[0]);
            Assert.AreEqual(22.13, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(75.26, world.Nodes[2].Value.Currencies[2]);
        }

        [TestMethod]
        public void TestExchange_ThreeCountries_Simultaneous()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);
            Country c3 = new Country(2);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddEdge(c1, c2);
            world.AddEdge(c2, c3);
            world.PrintState();
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(90, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(9, world.Nodes[0].Value.Currencies[1]);
            Assert.IsFalse(world.Nodes[0].Value.Currencies.ContainsKey(2));

            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(81, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[2]);

            Assert.IsFalse(world.Nodes[2].Value.Currencies.ContainsKey(0));
            Assert.AreEqual(10, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(90, world.Nodes[2].Value.Currencies[2]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(81.9, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(15.39, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(0.9, world.Nodes[0].Value.Currencies[2]);

            Assert.AreEqual(17.1, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(67.51, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(17.1, world.Nodes[1].Value.Currencies[2]);

            Assert.AreEqual(1, world.Nodes[2].Value.Currencies[0]);
            Assert.AreEqual(17.1, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(82, world.Nodes[2].Value.Currencies[2]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(75.25, Math.Round(world.Nodes[0].Value.Currencies[0], 2));
            Assert.AreEqual(19.93, Math.Round(world.Nodes[0].Value.Currencies[1], 2));
            Assert.AreEqual(2.35, Math.Round(world.Nodes[0].Value.Currencies[2], 2));

            Assert.AreEqual(22.14, Math.Round(world.Nodes[1].Value.Currencies[0], 2));
            Assert.AreEqual(57.93, Math.Round(world.Nodes[1].Value.Currencies[1], 2));
            Assert.AreEqual(22.14, Math.Round(world.Nodes[1].Value.Currencies[2], 2));

            Assert.AreEqual(2.61, Math.Round(world.Nodes[2].Value.Currencies[0], 2));
            Assert.AreEqual(22.14, Math.Round(world.Nodes[2].Value.Currencies[1], 2));
            Assert.AreEqual(75.51, Math.Round(world.Nodes[2].Value.Currencies[2], 2));
        }

        [TestMethod]
        public void TestExchange_FiveCountries()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);
            Country c3 = new Country(2);
            Country c4 = new Country(3);
            Country c5 = new Country(4);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddNode(c4);
            world.AddNode(c5);
            world.AddEdge(c1, c3);
            world.AddEdge(c2, c4);
            world.AddEdge(c3, c4);
            world.AddEdge(c4, c5);

            world.IterationsTillExchanged(printState: true);

            for (int i = 0; i < 5; i++)
            {
                double sum = 0;
                for (int j = 0; j < 5; j++)
                {
                    sum += world.Countries[j].Value.Currencies[i];
                }

                Assert.AreEqual(100, Math.Round(sum,0), "Amounts don't sum to 100 for currency " + i);
            }
        }

        [TestMethod]
        public void TestExchange_FiveCountries_Simultaneous()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);
            Country c3 = new Country(2);
            Country c4 = new Country(3);
            Country c5 = new Country(4);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddNode(c4);
            world.AddNode(c5);
            world.AddEdge(c1, c3);
            world.AddEdge(c2, c4);
            world.AddEdge(c3, c4);
            world.AddEdge(c4, c5);
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();
            world.IterationsTillExchanged(printState: true);

            for (int i = 0; i < 5; i++)
            {
                double sum = 0;
                for (int j = 0; j < 5; j++)
                {
                    sum += world.Countries[j].Value.Currencies[i];
                }

                Assert.AreEqual(100.00, Math.Round(sum, 2), "Amounts don't sum to 100 for currency " + i);
            }
        }

        [TestMethod]
        public void TestClone()
        {
            Country c1 = new Country(0);
            Country c2 = new Country(1);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddEdge(c1, c2);

            World buffer = (World)world.Clone();
            world.PrintState();

            Assert.AreNotSame(world, buffer);
            Assert.AreNotSame(world.Countries, buffer.Countries);
            Assert.AreNotSame(world.Countries[0], buffer.Countries[0]);
            Assert.AreNotSame(world.Countries[0].Neighbors, buffer.Countries[0].Neighbors);
            Assert.AreNotSame(world.Countries[0].Neighbors[0], buffer.Countries[0].Neighbors[0]);

            Assert.AreEqual(world.Countries[0].Value.Name, buffer.Countries[0].Value.Name);
            Assert.AreEqual(
                world.Countries[0].Neighbors[0].Value.Name, 
                buffer.Countries[0].Neighbors[0].Value.Name);
        }

        [TestMethod]
        public void TestCount_2x2()
        {
            Country c1 = new Country(1);
            Country c2 = new Country(1);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddEdge(c1, c2);

            Assert.AreEqual(2, world.Size);
        }

        [TestMethod]
        public void TestOneIslandCount_OneCountry()
        {
            Country c1 = new Country(1);
            
            World world = new World();
            world.AddNode(c1);

            Assert.AreEqual(1, world.IslandCount());
        }

        [TestMethod]
        public void TestTwoIslandCount_TwoCountries()
        {
            Country c1 = new Country(1);
            Country c2 = new Country(2);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);

            Assert.AreEqual(2, world.IslandCount());
        }

        [TestMethod]
        public void TestThreeIslandCount_SixCountries()
        {
            Country c1 = new Country(1);
            Country c2 = new Country(2);
            Country c3 = new Country(3);
            Country c4 = new Country(4);
            Country c5 = new Country(5);
            Country c6 = new Country(6);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddNode(c4);
            world.AddNode(c5);
            world.AddNode(c6);
            world.AddEdge(c1, c2);
            world.AddEdge(c3, c4);
            world.AddEdge(c5, c6);

            Assert.AreEqual(3, world.IslandCount());
        }

        [TestMethod]
        public void TestOneIslandCount_FourCountries()
        {
            Country c1 = new Country(1);
            Country c2 = new Country(2);
            Country c3 = new Country(3);
            Country c4 = new Country(4);

            World world = new World();
            world.AddNode(c1);
            world.AddNode(c2);
            world.AddNode(c3);
            world.AddNode(c4);
            world.AddEdge(c1, c2);
            world.AddEdge(c1, c3);
            world.AddEdge(c3, c4);

            Assert.AreEqual(1, world.IslandCount());
        }
    }
}
