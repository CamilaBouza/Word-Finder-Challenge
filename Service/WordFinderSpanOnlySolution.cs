namespace Service
{
    public sealed class WordFinderSpanOnlySolution : WordFinderBase
    {
        public WordFinderSpanOnlySolution(IEnumerable<string> matrix)
            : base(matrix)
        { }

        protected override short FindWordInsideMatrix(string word, IEnumerable<string> matrix)
        {
            short repetitionsPerWord = 0;

            foreach (var row in matrix)
            {
                repetitionsPerWord += (short)row.AsSpan().Count(word);
            }

            return repetitionsPerWord;
        }
    }
}
