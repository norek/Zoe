using System.Collections.Generic;

namespace TradeCaretaker.Tests
{
    public class RSI_INDEX_Equality : IEqualityComparer<RSI_INDEX>
    {
        private readonly int _rounding;

        private RSI_INDEX_Equality(int rounding)
        {
            _rounding = rounding;
        }

        public bool Equals(RSI_INDEX x, RSI_INDEX y)
        {
            return x.AvgGain.Rounded(_rounding) == y.AvgGain.Rounded(_rounding) &&
                   x.AvgLoss.Rounded(_rounding) == y.AvgLoss.Rounded(_rounding) &&
                   x.RS.Rounded(_rounding) == y.RS.Rounded(_rounding) &&
                   x.RSI.Rounded(_rounding) == y.RSI.Rounded(_rounding);
        }

        public int GetHashCode(RSI_INDEX obj)
        {
            return obj.GetHashCode();
        }

        public static RSI_INDEX_Equality Create(int rounding)
        {
            return new RSI_INDEX_Equality(rounding);
        }
    }
}