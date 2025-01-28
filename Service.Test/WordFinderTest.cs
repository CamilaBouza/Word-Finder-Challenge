using Service.Test;

namespace Service.Test1
{
    public class WordFinderTest
    {
        private const short MatrixMaxSize = 64;

        [Fact]
        public void SetMatrixSizeTo65x65AndGetError()
        {
            string set = "lmfgolmyxrwijkkygoudgtmapefnadpvlmfgolmyxrwijkkygoudgtmapefnadpva";
            IEnumerable<string> matrix = Enumerable.Repeat(set, MatrixMaxSize + 1);

            var exception = Assert.Throws<Exception>(() => new WordFinderSpanOnlySolution(matrix));

            Assert.Equal($"Matrix size must not be higher than {MatrixMaxSize}x{MatrixMaxSize}", exception.Message);

        }

        [Fact]
        public void SetMatrixSizeTo65x64AndGetError()
        {
            string set = "lmfgolmyxrwijkkygoudgtmapefnadpvlmfgolmyxrwijkkygoudgtmapefnadpva";
            IEnumerable<string> matrix = Enumerable.Repeat(set, MatrixMaxSize);

            var exception = Assert.Throws<Exception>(() => new WordFinderSpanOnlySolution(matrix));

            Assert.Equal($"Matrix size must not be higher than {MatrixMaxSize}x{MatrixMaxSize}", exception.Message);
        }

        [Fact]
        public void NoWordsFoundGetEmptySetOfStrings()
        {
            IEnumerable<string> wordStream = ["dog"];

            string set = "lmf";
            IEnumerable<string> matrix = Enumerable.Repeat(set, 3);

            var wordFinder = new WordFinderSpanOnlySolution(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.Empty(result);
        }

        [Fact]
        public void OneWordFoundOnceHorizontally()
        {
            IEnumerable<string> wordStream = ["dog", "cat"];

            IEnumerable<string> matrix = ["dog", "roc", "ogl", "myx"];

            var wordFinder = new WordFinderSpanOnlySolution(matrix);

            var result = wordFinder.Find(wordStream);

            var list = result.ToList();

            Assert.Single(list);
            Assert.True(list[0] == "dog");
        }

        [Fact]
        public void OneWordVerticallyOnceFound()
        {
            IEnumerable<string> wordStream = ["dog", "cat"];

            IEnumerable<string> matrix = ["adc", "roc", "ogl", "myx"];

            var wordFinder = new WordFinderSpanOnlySolution(matrix);

            var result = wordFinder.Find(wordStream);

            var list = result.ToList();

            Assert.Single(list);
            Assert.True(list[0] == "dog");
        }

        [Fact]
        public void OneWordVerticallyAndHorizontallyFound()
        {
            IEnumerable<string> wordStream = ["dog", "cat"];

            IEnumerable<string> matrix = ["adc", "dog", "agt", "myx"];

            var wordFinder = new WordFinderSpanOnlySolution(matrix);

            var result = wordFinder.Find(wordStream);

            var list = result.ToList();

            Assert.True(list.Count == 1);
            Assert.True(list[0] == "dog");
        }

        [Fact]
        public void AWordInWordStreamIsLongerThanMatrixGetsRemoved()
        {
            IEnumerable<string> wordStream = ["dog", "cat", "schedule"];

            IEnumerable<string> matrix = ["adc", "dog", "agt", "myx"];

            var wordFinder = new WordFinderSpanOnlySolution(matrix);

            var result = wordFinder.Find(wordStream);

            var list = result.ToList();

            Assert.Single(list);
            Assert.True(list[0] == "dog");
        }

        [Fact]
        public void AWordIsRepeatedInsideTheWordStreamGetsCountedOnce()
        {
            IEnumerable<string> wordStream = ["dog", "cat", "cat"];

            IEnumerable<string> matrix = ["adc", "dog", "agt", "myx"];

            var wordFinder = new WordFinderSpanOnlySolution(matrix);

            var result = wordFinder.Find(wordStream);

            var list = result.ToList();

            Assert.Single(list);
            Assert.True(list[0] == "dog");
        }

        [Fact]
        public void FindTop10MostRepeatedWordsFromWordStream()
        {
            IEnumerable<string> wordStream = ["cat", "dog", "bat", "sun", "car", "box", "hat", "run", "it", "is", "an", "on", "at", "to", "in", "by"];

            string set = "lcatcatmyxrwijdoggoudgtmapebatsunlmfcarmyxrwboxkongoudintmapehat";
            IEnumerable<string> matrix = Enumerable.Repeat(set, MatrixMaxSize);

            var wordFinder = new WordFinderSpanOnlySolution(matrix);

            var result = wordFinder.Find(wordStream);

            var list = result.ToList();

            var expectedList = new List<string>
            {
                "at", "cat", "dog", "bat", "sun", "car", "box", "hat", "on", "in"
            };

            Assert.True(list.Count == 10);
            Assert.Equal(expectedList, list);
        }

        [Fact(Skip = "Run manually and in release mode to see the benchmark results")]
        public void PerformanceRun()
        {
            WordFinderBenchmarks.Run();
        }
    }
}