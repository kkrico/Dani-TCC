namespace Dani_TCC.Core.Models.Algorithm
{
    public interface IFileParser<out T> where T : class
    {
        T Parse(string fileLocation);
    }
}