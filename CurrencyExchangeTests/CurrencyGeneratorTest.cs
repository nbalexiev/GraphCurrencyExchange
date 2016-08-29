namespace CurrencyExchangeTests
{
    using CurrencyExchange;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    [TestClass]
    public class CurrencyGeneratorTest
    {
        [TestMethod]
        public void TestGetOne()
        {
            Assert.AreEqual("A", CurrencyGenerator.GetCurrencySymbol(0));
            Assert.AreEqual("B", CurrencyGenerator.GetCurrencySymbol(1));
            Assert.AreEqual("C", CurrencyGenerator.GetCurrencySymbol(2));
        }

        [TestMethod]
        public void TestGetAllSingleLetters()
        {
            List<string> result = CurrencyGenerator.GetCurrencySymbols(26);

            Assert.AreEqual("Z", result.Last());
        }

        [TestMethod]
        public void TestGetDoubleLetter()
        {
            Assert.AreEqual("AA", CurrencyGenerator.GetCurrencySymbol(26));
            Assert.AreEqual("AB", CurrencyGenerator.GetCurrencySymbol(27));
            Assert.AreEqual("AC", CurrencyGenerator.GetCurrencySymbol(28));
        }

        [TestMethod]
        public void TestGetAllTripleLetters()
        {
            List<string> result = CurrencyGenerator.GetCurrencySymbols(78);
            Assert.AreEqual("Z", result[25]);
            Assert.AreEqual("AA", result[26]);
            Assert.AreEqual("AB", result[27]);
            Assert.AreEqual("AZ", result[51]);
            Assert.AreEqual("BAA", result[52]);
            Assert.AreEqual("BAZ", result[77]);
        }
    }
}
