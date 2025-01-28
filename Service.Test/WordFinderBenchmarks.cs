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
        private readonly WordFinder _wordFinder = new(Enumerable.Repeat("lcatcatmyxrwijdoggoudgtmapefnadpvlmfgolmyxrwijkkygoudgtmapefnuio", 64));
        private readonly IEnumerable<string> _wordStream = ["cat", "dog", "bat", "sun", "car", "box", "hat", "run", "it", "is", "an", "on", "at", "to", "in", "by"];

        public static void Run()
        {
            BenchmarkRunner.Run<WordFinderBenchmarks>();
        }

        [Benchmark(Baseline = true)]
        public void WordFinderBenchmark()
        {
            _wordFinder.Find(_wordStream);
        }

        [Benchmark]
        public void WordFinderBenchmark2()
        {
            _wordFinder.FindRegexSolution(_wordStream);
        }
    }
}
