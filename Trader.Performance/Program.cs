using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace Trader.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RSI_Strategy_Perf>();

            Console.WriteLine("Whatsuup?");
        }
    }

    public class RSI_Strategy_Perf
    {
        [Benchmark]
        public void Do()
        {
            Task.Delay(200).Wait();
        }
    }
}
