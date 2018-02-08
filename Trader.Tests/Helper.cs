using System;
using System.Linq;
using Trader.Exchanges;

namespace Trader.Tests
{
    public static class Helper
    {
        public static decimal Rounded(this decimal value, int places = 2)
        {
            return Math.Round(value, places);
        }

        public static Trade[] AsTradesWitRates(this decimal[] values)
        {
            return values.Select(s => new Trade() { Rate = s }).ToArray();
        }
    }
}
