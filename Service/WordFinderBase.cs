using Service.Exceptions;
using Service.Interfaces;
using Service.Models;
using System.Text;

namespace Service
{
    public abstract class WordFinderBase : IWordFinder
    {
        private readonly short _rowsSize = 0;
        private readonly short _columnsSize = 0;
        private const short MatrixMaxSize = 64;
        private readonly IEnumerable<string> _matrix;
        private readonly IEnumerable<string> _invertedMatrix;

        protected WordFinderBase(IEnumerable<string> matrix)
        {
            _rowsSize = (short)matrix.Count();
            _columnsSize = (short)matrix.First().Length;

            if (MatrixExceedsMaxSize())
            {
                throw new MatrixSizeException($"Matrix size must not be higher than {MatrixMaxSize}x{MatrixMaxSize}");
            }

            _matrix = matrix;
            _invertedMatrix = GetInvertedMatrix();
        }


        /// <summary>
        /// Returns up to ten words from an IEnumerable<string> that are found the most times in a matrix
        /// </summary>
        /// <param name="wordStream"></param>
        public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            IEnumerable<string> distinctWordStream = wordStream.Distinct();
            IList<WordCount> wordCountList = [];

            foreach (string word in distinctWordStream)
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

        /// <summary>
        /// Validates if the size of the rows or the columns are bigger than the maximum size permitted
        /// </summary>
        private bool MatrixExceedsMaxSize()
        {
            return _rowsSize > MatrixMaxSize || _columnsSize > MatrixMaxSize;
        }

        /// <summary>
        /// Gets a new matrix where its rows are the original matrix's columns and its columns are the original matrix's rows
        /// </summary>
        private IEnumerable<string> GetInvertedMatrix()
        {
            IList<string> auxMatrix = [];

            for (short character = 0; character < _columnsSize; character++)
            {
                var invertedMatrixRowStringBuilder = new StringBuilder();

                foreach (string matrixRow in _matrix)
                {
                    invertedMatrixRowStringBuilder.Append(matrixRow[character]);
                }

                auxMatrix.Add(invertedMatrixRowStringBuilder.ToString());
            }

            return auxMatrix;
        }

        /// <summary>
        /// Searches for a word, counting how many times it gets found inside a matrix
        /// </summary>
        /// <param name="word"></param>
        private short Search(string word)
        {
            short result = 0;

            if (word.Length <= _rowsSize)
            {
                result += FindWordInsideMatrix(word, _matrix);
            }

            if (word.Length <= _columnsSize)
            {
                result += FindWordInsideMatrix(word, _invertedMatrix);
            }

            return result;
        }

        /// <summary>
        /// Returns the amount of times a word is in a matrix
        /// </summary>
        /// <param name="word"></param>
        /// <param name="matrix"></param>
        protected abstract short FindWordInsideMatrix(string word, IEnumerable<string> matrix);
    }
}
