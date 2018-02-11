using System;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace TradeCaretaker.Performance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Summary summary = BenchmarkRunner.Run<SmaBenchmark>();

            Console.ReadLine();
        }
    }
}