using System;
using System.Collections.Generic;
using System.Text;

namespace Trader
{
    public class SMA
    {
        private readonly int _length;
        decimal[] _buffer;
        int _index = 0;
        int _limit = 0;

        public SMA(int length)
        {
            _length = length;
            //declare one element bigger array to avoid overflow exception on shifting
            _buffer = new decimal[_length + 1];
            _limit = _length - 1;
        }

        public bool InitRun => _index < _limit;

        public decimal Calculate(decimal value)
        {
            _buffer[_index] = value;

            if (_index < _limit)
            {
                _index++;
                return 0;
            }

            decimal sma = 0;

            for (int i = 0; i < _length; i++)
            {
                sma += _buffer[i];
                _buffer[i] = _buffer[i + 1];
            }

            return sma / _length; ;
        }

        public static decimal CalculateAvarage(decimal[] values, int length)
        {
            decimal sun = 0;

            for (int i = 0; i < length; i++)
            {
                sun += values[i];
            }

            return sun / length;
        }
    }
}
