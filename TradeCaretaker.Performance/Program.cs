using System;
using BenchmarkDotNet.Running;

namespace TradeCaretaker.Performance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<RSI_Strategy_Perf>();

            Console.WriteLine("Whatsuup?");
            Console.ReadLine();
        }
    }
}