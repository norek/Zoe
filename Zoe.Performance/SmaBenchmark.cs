using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using Zoe.Overlays;

namespace Zoe.Performance
{
    [MemoryDiagnoser]
    public class SmaBenchmark
    {
        private decimal[] _input;

        [GlobalSetup]
        public void Setup()
        {
            Random rand = new Random();
            _input = Enumerable.Repeat(0, 1000).Select(x => (decimal)rand.Next(1, 100)).ToArray();
        }

        [Params(1, 100, 1000)]
        public int SMA_LENGTH { get; set; }

        [Benchmark(Baseline = true)]
        public decimal Do()
        {
            return SMA.CalculateAvarage(_input, SMA_LENGTH);
        }
    }
}
/*
 *  Method | SMA_LENGTH |         Mean |       Error |      StdDev | Allocated |
------- |----------- |-------------:|------------:|------------:|----------:|
     Do |          1 |     53.93 ns |   0.1524 ns |   0.1190 ns |       0 B |
     Do |        100 |  1,395.98 ns |  10.8399 ns |   9.0518 ns |       0 B |
     Do |       1000 | 13,125.26 ns | 159.8251 ns | 149.5004 ns |       0 B |
 */
