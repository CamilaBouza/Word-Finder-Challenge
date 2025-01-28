using Service.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace Service
{
    public sealed class WordFinder
    {
        private readonly IEnumerable<string> _matrix;
        private readonly IEnumerable<string> _invertedMatrix;
        private readonly short _matrixSize = 0;

        private const short MatrixMaxSize = 64;

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrixSize = (short)matrix.Count();

            if (MatrixExceedsMaxSize() || MatrixIsNotSquare(matrix))
            {
                throw new Exception($"Matrix size and content must not be higher than {MatrixMaxSize}");
            }

            _matrix = matrix;

            _invertedMatrix = GetInvertedMatrix();
        }

        private bool MatrixIsNotSquare(IEnumerable<string> matrix)
        {
            return matrix.Any(x => x.Length != _matrixSize);
        }

        private bool MatrixExceedsMaxSize()
        {
            return _matrixSize > MatrixMaxSize;
        }

        #region while solution part
        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var verifiedWordStream = WordsVerifier(wordStream);

            IList<WordCount> wordCountList = [];

            foreach (string word in verifiedWordStream)
            {
                var timesFound = Search(word);

                if (timesFound > 0)
                {
                    wordCountList.Add(
                    new WordCount
                    {
                        Word = word,
                        TimesFound = timesFound
                    });
                }
            }

            if (wordCountList.Count == 0)
            {
                return [];
            }

            IEnumerable<string> resultFind = wordCountList.OrderByDescending(x => x.TimesFound).Take(10).Select(x => x.Word);

            return resultFind;
        }

        private short Search(string word)
        {
            var result = 0;
            result += FindWordInsideMatrix(word, _matrix);
            result += FindWordInsideMatrix(word, _invertedMatrix);

            return (short)result;
        }

        private static short FindWordInsideMatrix(string word, IEnumerable<string> matrix)
        {
            short repetitionsPerWord = 0;
            short position;
            short auxPosition;
            short wordLength = (short)word.Length;

            foreach (var row in matrix)
            {
                position = 0;
                while (position + wordLength <= row.Length)
                {
                    auxPosition = (short)row.IndexOf(word, position, wordLength);
                    if (auxPosition >= 0)
                    {
                        repetitionsPerWord++;
                        position = (short)(position + wordLength);
                    }
                    else { position += 1; }
                }
            }

            return repetitionsPerWord;
        }
        #endregion while solution part

        #region Regex solution part
        public IEnumerable<string> FindRegexSolution(IEnumerable<string> wordStream)
        {
            var verifiedWordStream = WordsVerifier(wordStream);

            IList<WordCount> wordCountList = [];

            foreach (string word in verifiedWordStream)
            {
                var timesFound = SearchRegexSolution(word);

                if (timesFound > 0)
                {
                    wordCountList.Add(
                    new WordCount
                    {
                        Word = word,
                        TimesFound = timesFound
                    });
                }
            }

            if (wordCountList.Count == 0)
            {
                return [];
            }

            IEnumerable<string> resultFind = wordCountList.OrderByDescending(x => x.TimesFound).Take(10).Select(x => x.Word);

            return resultFind;
        }

        private short SearchRegexSolution(string word)
        {
            var result = 0;
            Regex regex = new(word);

            result += FindWordInsideMatrixRegexSolution(regex, _matrix);
            result += FindWordInsideMatrixRegexSolution(regex, _invertedMatrix);

            return (short)result;
        }

        private static short FindWordInsideMatrixRegexSolution(Regex regex, IEnumerable<string> matrix)
        {
            short repetitionsPerWord = 0;

            foreach (var row in matrix)
            {
                repetitionsPerWord += (short)regex.Matches(row).Count;
            }

            return repetitionsPerWord;
        }
        #endregion Regex solution part

        private IEnumerable<string> GetInvertedMatrix()
        {
            IList<string> auxMatrix = [];

            for (short i = 0; i < _matrixSize; i++)
            {
                var columnStringBuilder = new StringBuilder();

                foreach (string row in _matrix)
                {
                    columnStringBuilder.Append(row[i]);
                }

                auxMatrix.Add(columnStringBuilder.ToString());
            }

            return auxMatrix;
        }

        private IEnumerable<string> WordsVerifier(IEnumerable<string> wordStream)
        {
            return wordStream.Where(word => word.Length <= _matrixSize).Distinct();
        }
    }
}
