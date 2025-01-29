namespace Service.Exceptions
{
    public class MatrixSizeException : Exception
    {
        public MatrixSizeException(string errorMessage)
            : base(errorMessage) { }
    }
}
