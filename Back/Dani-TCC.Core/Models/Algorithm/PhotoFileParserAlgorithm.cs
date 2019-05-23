namespace Dani_TCC.Core.Models.Algorithm
{
    public class PhotoFileParserAlgorithm : IFileParserAlgorithm<Photo>
    {
        public Photo Parse(string fileLocation)
        {
            var photoLocation = new PhotoLocation(fileLocation);
            return photoLocation;
        }
    }
}