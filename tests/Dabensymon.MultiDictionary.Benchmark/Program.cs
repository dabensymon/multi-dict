using BenchmarkDotNet.Running;

namespace Dabensymon.MultiDictionary.Tests.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchmarkTests>();
        }
    }
}