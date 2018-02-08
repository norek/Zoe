using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trader.Exchanges;

namespace Trader
{
    public class RSI_Strategy
    {
        private readonly int _overbought;
        private readonly int _oversold;
        RSI _rsi = new RSI(14);

        public RSI_Strategy(int overbought = 70, int oversold = 30)
        {
            _overbought = overbought;
            _oversold = oversold;
        }

        public void OnPeriod(Span<Trade> trades)
        {
            _rsi.Calculate(trades.Slice(0, _rsi._periods));
        }


        private bool CollectionData => _rsi.CollectingData;

        public override string ToString()
        {
            if (CollectionData)
            {
                return "Collection data...";
            }

            return _rsi.Current.RSI.ToString();
        }
    }
}
