``` ini

BenchmarkDotNet=v0.10.12, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.192)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical cores and 4 physical cores
Frequency=2742189 Hz, Resolution=364.6722 ns, Timer=TSC
.NET Core SDK=2.1.4
  [Host]     : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.5 (Framework 4.6.26020.03), 64bit RyuJIT


```
|      Method |     Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------ |---------:|----------:|----------:|-------:|-------:|----------:|
| Do_WithSpan | 1.014 us | 0.0199 us | 0.0205 us | 0.0172 | 0.0076 |     104 B |
