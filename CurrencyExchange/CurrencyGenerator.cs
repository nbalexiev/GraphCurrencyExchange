using System.Collections.Generic;

namespace CurrencyExchange
{
    public class CurrencyGenerator
    {
        private static readonly int lettersCount = 'Z' - 'A' + 1;

        public static List<string> GetCurrencySymbols(int size)
        {
            List<string> symbols = new List<string>();
            for (int i = 0; i<size; i++)
            {
                symbols.Add(GetCurrencySymbol(i));
            }

            return symbols;
        }

        public static string GetCurrencySymbol(int code)
        {
            string symbol = "";
            while (code >= 0)
            {
                symbol += (char)('A' + (code >= lettersCount ? (code / lettersCount)-1 : code));
                code -= lettersCount;
            }

            return symbol;
        }
    }
}
