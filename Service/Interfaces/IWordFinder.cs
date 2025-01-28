namespace Service.Interfaces
{
    public interface IWordFinder
    {
        public IEnumerable<string> Find(IEnumerable<string> wordStream);
    }
}
