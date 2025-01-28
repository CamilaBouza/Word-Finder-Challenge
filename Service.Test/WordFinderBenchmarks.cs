using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace Service.Test
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class WordFinderBenchmarks
    {
        private const string Set = "lcatcatmyxrwijdoggoudgtmapefnadpvlmfgolmyxrwijkkygoudgtmapefnuio";
        private readonly WordFinderIndexOfSolution _wordFinderIndexOf = new(Enumerable.Repeat(Set, 64));
        private readonly WordFinderRegexSolution _wordFinderRegex = new(Enumerable.Repeat(Set, 64));
        private readonly WordFinderSpanOnlySolution _wordFinderSpanOnly = new(Enumerable.Repeat(Set, 64));
        private readonly IEnumerable<string> _wordStream = ["cat", "dog", "bat", "sun", "car", "box", "hat", "run", "it", "is", "an", "on", "at", "to", "in", "by"];

        public static void Run()
        {
            BenchmarkRunner.Run<WordFinderBenchmarks>();
        }

        [Benchmark(Baseline = true)]
        public void WordFinderBenchmarkSpanOnly()
        {
            _wordFinderSpanOnly.Find(_wordStream);
        }

        [Benchmark]
        public void WordFinderBenchmarkIndexOf()
        {
            _wordFinderIndexOf.Find(_wordStream);
        }

        [Benchmark]
        public void WordFinderBenchmarkRegexSolution()
        {
            _wordFinderRegex.Find(_wordStream);
        }
    }
}
