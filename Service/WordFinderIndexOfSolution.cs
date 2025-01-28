namespace Service
{
    public sealed class WordFinderIndexOfSolution : WordFinderBase
    {
        public WordFinderIndexOfSolution(IEnumerable<string> matrix)
            : base(matrix)
        { }

        protected override short FindWordInsideMatrix(string word, IEnumerable<string> matrix)
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
    }
}
