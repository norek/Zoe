using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Linq;
using Trader.Exchanges;

namespace Trader.Performance
{
    [MemoryDiagnoser]
    public class RSI_Strategy_Perf
    {
        Trade[] _inputStream;
        RSI_Strategy strategy = new RSI_Strategy();

        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random();
            _inputStream = Enumerable.Repeat<Trade>(new Trade(), 1000).Select(w =>
            {
                w.Rate = (decimal)random.NextDouble(); return w;
            }).ToArray();

            for (int i = 0; i < 20; i++)
            {
                strategy.Execute(_inputStream);
            }
        }

        [Benchmark]
        public void Do_WithSpan()
        {
            strategy.Execute(_inputStream.AsSpan().Slice(0,20));
        }
    }
}
