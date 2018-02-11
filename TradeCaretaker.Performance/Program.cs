using System;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace TradeCaretaker.Performance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Summary summary = BenchmarkRunner.Run<SmaBenchmark>();
            Summary summary = BenchmarkRunner.Run<PerioderBenchmark>();

            Console.ReadLine();
        }
    }
}