using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Zoe.Exchanges;
using Zoe.Strategies;

namespace Zoe.Performance
{
    [MemoryDiagnoser]
    public class RSI_Strategy_Perf
    {
        private Trade[] _inputStream;
        private readonly RSI_Strategy strategy = new RSI_Strategy();

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            _inputStream = Enumerable.Repeat(new Trade(), 1000).Select(w =>
            {
                w.Rate = (decimal) random.NextDouble();
                return w;
            }).ToArray();

            for (var i = 0; i < 20; i++) strategy.Execute(_inputStream.AsSpan());
        }

        [Benchmark]
        public void Do_WithSpan()
        {
            strategy.Execute(_inputStream.AsSpan().Slice(0, 20));
        }
    }
}