using System.Text.RegularExpressions;

namespace Service
{
    public sealed class WordFinderRegexSolution : WordFinderBase
    {
        public WordFinderRegexSolution(IEnumerable<string> matrix)
            : base(matrix)
        { }

        protected override short FindWordInsideMatrix(string word, IEnumerable<string> matrix)
        {
            short repetitionsPerWord = 0;
            var regex = new Regex(word);

            foreach (var row in matrix)
            {
                repetitionsPerWord += (short)regex.Count(row.AsSpan());
            }

            return repetitionsPerWord;
        }
    }
}
