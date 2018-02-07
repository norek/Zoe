using System;

namespace Trader.Tests
{
    public static class Helper
    {
        public static decimal Rounded(this decimal value, int places = 2)
        {
            return Math.Round(value, places);
        }
    }
}
