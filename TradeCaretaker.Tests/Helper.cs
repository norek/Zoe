using System;
using System.Linq;
using TradeCaretaker.Exchanges;

namespace TradeCaretaker.Tests
{
    public static class Helper
    {
        public static decimal Rounded(this decimal value, int places = 2)
        {
            return Math.Round(value, places);
        }

        public static Span<Trade> AsTradesWitRates(this decimal[] values)
        {
            return values.Select(s => new Trade {Rate = s}).ToArray().AsSpan();
        }
    }
}