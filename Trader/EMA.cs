using System;
using System.Collections.Generic;
using System.Text;

namespace Trader
{
    public class EMA
    {
        private readonly decimal _multiplier;
        private readonly int _periods;
        LinkedList<decimal> _ema_buffer = new LinkedList<decimal>();

        decimal[] valueBuffer;

        public EMA(int periods = 12)
        {
            if (periods <= 0)
                throw new ArgumentException();

            _multiplier = (decimal)2 / (periods + 1);
            _periods = periods;
            valueBuffer = new decimal[_periods];
        }

        public bool CollectingData => _ema_buffer.Count == 0;

        public void Calculate(decimal[] values)
        {
            if (values.Length + 1 < _periods)
                return;

            if (CollectingData)
            {
                var sma = SMA.CalculateAvarage(values, _periods);
                _ema_buffer.AddFirst(sma);
                return;
            }

            var newEma = (values[values.Length - 1] - _ema_buffer.First.Value) * _multiplier + _ema_buffer.First.Value;
            _ema_buffer.AddFirst(newEma);
        }

        public decimal Current => _ema_buffer.First.Value;

        public decimal Previous => _ema_buffer.First.Previous.Value;
    }
}
