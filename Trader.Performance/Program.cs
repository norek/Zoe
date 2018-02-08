using BenchmarkDotNet.Running;
using System;

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
}
