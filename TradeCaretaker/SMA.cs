namespace TradeCaretaker
{
    public class SMA
    {
        private readonly int _length;
        private readonly decimal[] _buffer;
        private int _index;
        private readonly int _limit;

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

            for (var i = 0; i < _length; i++)
            {
                sma += _buffer[i];
                _buffer[i] = _buffer[i + 1];
            }

            return sma / _length;
            ;
        }

        public static decimal CalculateAvarage(decimal[] values, int length)
        {
            decimal sun = 0;

            for (var i = 0; i < length; i++) sun += values[i];

            return sun / length;
        }
    }
}