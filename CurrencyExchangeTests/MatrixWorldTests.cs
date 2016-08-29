using CurrencyExchange;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyExchangeTests
{
    using System;

    [TestClass]
    public class MatrixMatrixWorldTests
    {
        [TestMethod]
        public void TestExchangeIterations_TwoCountries()
        {
            Country[,] map =
                {
                    { new Country(0), null }, 
                    { new Country(1), null }
                };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SequentialCurrencyExchanger();

            Assert.AreEqual(1, world.IterationsTillExchanged());
        }

        [TestMethod]
        public void TestExchangeIterations_FiveCountries()
        {
            Country[,] map = {
                { null, null, null, new Country(0) },
                { new Country(1), null, new Country(2), null },
                { null, new Country(3), null, null },
                { null, null, new Country(4), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SequentialCurrencyExchanger();

            Assert.AreEqual(3, world.IterationsTillExchanged());
        }

        [TestMethod]
        public void TestExchangeIterations_FiveCountries_Simulteneous()
        {
            Country[,] map = {
                { null, null, null, new Country(0) },
                { new Country(1), null, new Country(2), null },
                { null, new Country(3), null, null },
                { null, null, new Country(4), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();

            Assert.AreEqual(3, world.IterationsTillExchanged());
        }

        [TestMethod]
        public void TestExchange_ThreeCountries()
        {
            Country[,] map = {
                { new Country(0), null, null },
                { new Country(1), null, null },
                { null, new Country(2), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SequentialCurrencyExchanger();

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(1, world.Nodes[2].Value.Currencies[0]);
            Assert.AreEqual(9, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(90, world.Nodes[2].Value.Currencies[2]);

            Assert.AreEqual(9, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(81, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[2]);

            Assert.AreEqual(90, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(10, world.Nodes[0].Value.Currencies[1]);
            Assert.IsFalse(world.Nodes[0].Value.Currencies.ContainsKey(2));

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(2.61, world.Nodes[2].Value.Currencies[0]);
            Assert.AreEqual(15.49, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(81.9, world.Nodes[2].Value.Currencies[2]);

            Assert.AreEqual(15.49, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(67.41, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(17.1, world.Nodes[1].Value.Currencies[2]);

            Assert.AreEqual(81.9, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(17.1, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(1, world.Nodes[0].Value.Currencies[2]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(4.56, world.Nodes[2].Value.Currencies[0]);
            Assert.AreEqual(20.18, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(75.26, world.Nodes[2].Value.Currencies[2]);

            Assert.AreEqual(20.18, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(57.69, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(22.13, world.Nodes[1].Value.Currencies[2]);

            Assert.AreEqual(75.26, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(22.13, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(2.61, world.Nodes[0].Value.Currencies[2]);
        }

        [TestMethod]
        public void TestExchange_ThreeCountries_Simultaneous()
        {
            Country[,] map = {
                { new Country(0), null, null },
                { new Country(1), null, null },
                { null, new Country(2), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();
            world.Exchange();
            world.PrintState();

            Assert.AreEqual(90, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(10, world.Nodes[0].Value.Currencies[1]);
            Assert.IsFalse(world.Nodes[0].Value.Currencies.ContainsKey(2));

            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(81, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(10, world.Nodes[1].Value.Currencies[2]);

            Assert.IsFalse(world.Nodes[2].Value.Currencies.ContainsKey(0));
            Assert.AreEqual(9, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(90, world.Nodes[2].Value.Currencies[2]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(82, world.Nodes[0].Value.Currencies[0]);
            Assert.AreEqual(17.1, world.Nodes[0].Value.Currencies[1]);
            Assert.AreEqual(1, world.Nodes[0].Value.Currencies[2]);

            Assert.AreEqual(17.1, world.Nodes[1].Value.Currencies[0]);
            Assert.AreEqual(67.51, world.Nodes[1].Value.Currencies[1]);
            Assert.AreEqual(17.1, world.Nodes[1].Value.Currencies[2]);

            Assert.AreEqual(0.9, world.Nodes[2].Value.Currencies[0]);
            Assert.AreEqual(15.39, world.Nodes[2].Value.Currencies[1]);
            Assert.AreEqual(81.9, world.Nodes[2].Value.Currencies[2]);

            world.Exchange();
            world.PrintState();

            Assert.AreEqual(75.51, Math.Round(world.Nodes[0].Value.Currencies[0], 2));
            Assert.AreEqual(22.14, Math.Round(world.Nodes[0].Value.Currencies[1], 2));
            Assert.AreEqual(2.61, Math.Round(world.Nodes[0].Value.Currencies[2], 2));

            Assert.AreEqual(22.14, Math.Round(world.Nodes[1].Value.Currencies[0], 2));
            Assert.AreEqual(57.93, Math.Round(world.Nodes[1].Value.Currencies[1], 2));
            Assert.AreEqual(22.14, Math.Round(world.Nodes[1].Value.Currencies[2], 2));

            Assert.AreEqual(2.35, Math.Round(world.Nodes[2].Value.Currencies[0], 2));
            Assert.AreEqual(19.93, Math.Round(world.Nodes[2].Value.Currencies[1], 2));
            Assert.AreEqual(75.25, Math.Round(world.Nodes[2].Value.Currencies[2], 2));
        }

        [TestMethod]
        public void TestExchange_TwoCountries_2x2()
        {
            Country[,] map = {
                { new Country(0), null },
                { new Country(1), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SequentialCurrencyExchanger();

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
        public void TestExchange_TwoCountries_2x2_Simulteneous()
        {
            Country[,] map = {
                { new Country(0), null },
                { new Country(1), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();

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
        public void TestExchange_FiveCountries_4x4()
        {
            Country[,] map = {
                { null, null, null, new Country(0) },
                { new Country(1), null, new Country(2), null },
                { null, new Country(3), null, null },
                { null, null, new Country(4), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.PrintState();
            world.CurrencyExchangeStrategy = new SequentialCurrencyExchanger();

            world.IterationsTillExchanged(printState: true);

            for (int i = 0; i < 5; i++)
            {
                double sum = 0;
                for (int j = 0; j < 5; j++)
                {
                    sum += world.Nodes[j].Value.Currencies[i];
                }

                Assert.AreEqual(100.00, Math.Round(sum, 2), "Amounts don't sum to 100 for currency " + i);
            }
        }

        [TestMethod]
        public void TestExchange_FiveCountries_4x4_Simulteneous()
        {
            Country[,] map = {
                { null, null, null, new Country(0) },
                { new Country(1), null, new Country(2), null },
                { null, new Country(3), null, null },
                { null, null, new Country(4), null }
            };

            MatrixWorld world = new MatrixWorld(map);
            world.CurrencyExchangeStrategy = new SimultaneousCurrencyExchanger();
            world.PrintState();

            world.IterationsTillExchanged(printState: true);

            for (int i = 0; i < 5; i++)
            {
                double sum = 0;
                for (int j = 0; j < 5; j++)
                {
                    sum += world.Nodes[j].Value.Currencies[i];
                }

                Assert.AreEqual(100.00, Math.Round(sum, 2), "Amounts don't sum to 100 for currency " + i);
            }
        }

        [TestMethod]
        public void TestCount_2x2()
        {
            Country[,] map = {
                { null, null },
                { null, null }
            };

            MatrixWorld world = new MatrixWorld(map);

            Assert.AreEqual(0, world.CountryCount());

            map[0, 1] = new Country(1);
            //Assert.AreEqual(1, world.CountryCount());
            map[0, 0] = new Country(0);
            //Assert.AreEqual(2, world.CountryCount());
            map[1, 0] = new Country(2);
            Assert.AreEqual(3, world.CountryCount());
            map[1, 1] = new Country(3);
            //Assert.AreEqual(4, world.CountryCount());
        }


        [TestMethod]
        public void TestCount_4x4()
        {
            Country[,] map = {
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
                { null, null, null, null },
            };

            MatrixWorld world = new MatrixWorld(map);

            Assert.AreEqual(0, world.CountryCount());

            map[3, 3] = new Country(1);
            Assert.AreEqual(1, world.CountryCount());
            map[0, 0] = new Country(0);
            Assert.AreEqual(2, world.CountryCount());
            map[2, 0] = new Country(2);
            Assert.AreEqual(3, world.CountryCount());
            map[1, 1] = new Country(3);
            Assert.AreEqual(4, world.CountryCount());
        }

        [TestMethod]
        public void TestOneIslandCount_TwoCountries_2x2()
        {
            Country[,] map = {
                { null, new Country(1) },
                { new Country(2), null }
            };

            MatrixWorld world = new MatrixWorld(map);

            Assert.AreEqual(1, world.IslandCount());
        }

        [TestMethod]
        public void TestTwoIslandCount_TwoCountries_4x4()
        {
            Country[,] map = {
                { null, new Country(1), null, null },
                { null, null, null, null },
                { null, new Country(9), null, null },
                { null, null, null, null },
            };

            MatrixWorld world = new MatrixWorld(map);

            Assert.AreEqual(2, world.IslandCount());
        }

        [TestMethod]
        public void TestThreeIslandCount_ThreeCountries_4x4()
        {
            Country[,] map = {
                { null, null, null,new Country(3) },
                { new Country(4), null, null, null },
                { null, null, null, null },
                { null, null, new Country(14), null },
            };

            MatrixWorld world = new MatrixWorld(map);

            Assert.AreEqual(3, world.IslandCount());
        }

        [TestMethod]
        public void TestOneIslandCount_FourCountries_4x4()
        {
            Country[,] map = {
                { null, new Country(1), null, null },
                { new Country(4), null, null, null },
                { null, new Country(9), null, null },
                { null, null, new Country(14), null },
            };

            MatrixWorld world = new MatrixWorld(map);

            Assert.AreEqual(1, world.IslandCount());
        }

    }
}
