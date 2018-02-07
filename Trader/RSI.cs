using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trader
{
    public class RSI
    {
        private readonly int _periods;
        private int _inputLength;
        LinkedList<RSI_INDEX> _rsiMove = new LinkedList<RSI_INDEX>();

        public RSI(int periods = 14)
        {
            if (periods <= 0)
                throw new ArgumentException();

            _periods = periods;
            _inputLength = _periods + 1;
        }

        public void Calculate(decimal[] values)
        {
            if (values.Length < _inputLength)
                return;

            var currentPrice = values[values.Length - 1];
            var previous = values[values.Length - 2];

            var diffInPrice = currentPrice - previous;

            decimal avgGain = 0;
            decimal avgLoss = 0;

            if (_rsiMove.Count == 0)
            {
                decimal sumOfGains = 0;
                decimal sumOfLosses = 0;

                for (int i = 0; i < _inputLength - 1; i++)
                {
                    var prev = values[values.Length - _inputLength + i];
                    var next = values[values.Length - _inputLength + i + 1];

                    var diff = next - prev;

                    if (diff > 0)
                    {
                        sumOfGains += diff;
                    }
                    else if (diff < 0)
                    {
                        sumOfLosses += (-1) * diff;
                    }
                }

                avgGain = sumOfGains / _periods;
                avgLoss = sumOfLosses / _periods;
            }
            else
            {
                var currentIndex = _rsiMove.First.Value;

                var currentGain = diffInPrice > 0 ? diffInPrice : 0;
                var currentLosse = diffInPrice < 0 ? (-1) * diffInPrice : 0;

                avgGain = ((currentIndex.AvgGain * (_periods - 1)) + currentGain) / _periods;
                avgLoss = ((currentIndex.AvgLoss * (_periods - 1)) + currentLosse) / _periods;
            }

            decimal rs = 0;
            decimal rsi = 0;

            if (avgLoss == 0)
            {
                rs = 100;
                rsi = 100;
            }
            else if (avgGain != 0)
            {
                rs = (avgGain / _periods) / (avgLoss / _periods);
                rsi = 100 - (100 / (1 + rs));
            }

            RSI_INDEX index = new RSI_INDEX
            {
                RSI = rsi,
                RS = rs,
                AvgGain = avgGain,
                AvgLoss = avgLoss
            };

            _rsiMove.AddFirst(index);
        }

        public bool CollectingData => _rsiMove.Count == 0;
        public IEnumerable<RSI_INDEX> Values => _rsiMove.ToList();

        public RSI_INDEX Current => _rsiMove.First.Value;
    }

    public struct RSI_INDEX
    {
        public decimal RSI { get; set; }
        public decimal RS { get; set; }
        public decimal AvgGain { get; set; }
        public decimal AvgLoss { get; set; }
    }
}
