using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trader.Exchanges;

namespace Trader.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RSI_Strategy_Perf>();

            Console.WriteLine("Whatsuup?");
            Console.ReadLine();
        }
    }

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
                strategy.OnPeriod(_inputStream);
            }
        }

        //[Benchmark(Baseline = true)]
        //public void Do()
        //{
        //    strategy.OnPeriod(_inputStream.ToArray());
        //}

        [Benchmark]
        public void Do_WithSpan()
        {
            strategy.OnPeriod(_inputStream.AsSpan().Slice(0,20));
        }
    }
}
