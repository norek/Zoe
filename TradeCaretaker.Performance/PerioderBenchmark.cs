using BenchmarkDotNet.Attributes;
using System;
using System.Threading.Tasks;
using TradeCaretaker.Exchanges;

namespace TradeCaretaker.Performance
{
    [MemoryDiagnoser]
    public class PerioderBenchmark
    {
        private Trade[] _input;

        [GlobalSetup]
        public void Setup()
        {
            int numberOfElements = 1000;
            _startDate = new DateTime(2010, 10, 10, 10, 10, 0);
            _input = new Trade[numberOfElements];

            _endDate = _startDate;
            for (int i = 0; i < numberOfElements; i++)
            {
                _endDate = _endDate.AddSeconds(10);

                _input[i] = new Trade() { Date = _endDate, Rate = 3 };
            }
        }

        private readonly Perioder _perioder;

        private DateTime _startDate;
        private DateTime _endDate;

        public PerioderBenchmark()
        {
            _perioder = new Perioder();
        }

        [Benchmark(Baseline = true)]
        public decimal Do()
        {

            _perioder.Periodify(_input, _startDate, _endDate, 5, (date, trade) => Task.FromResult(0)).Wait();
            return 2;
        }
    }
}
