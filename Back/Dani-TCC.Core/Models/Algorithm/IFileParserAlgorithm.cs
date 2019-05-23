namespace Dani_TCC.Core.Models.Algorithm
{
    public interface IFileParserAlgorithm<out T> where T : class
    {
        T Parse(string fileLocation);
    }
    
    public class PhotoFileParser : IFileParserAlgorithm<Photo>
    {
        public Photo Parse(string fileLocation)
        {
            throw new System.NotImplementedException();
        }
    }
}