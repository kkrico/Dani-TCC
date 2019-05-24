using System.Collections.Generic;

namespace Dani_TCC.Core.Models
{
    public partial class Photo
    {
        public Photo()
        {
            Valueanswer = new HashSet<Valueanswer>();
        }

        public int Idphoto { get; set; }
        public int? Idgender { get; set; }
        public int? Idethnicity { get; set; }
        public string Photohash { get; set; }
        public byte Elected { get; set; }
        public byte[] FileContents { get; set; }
        public string PhotoName { get; set; }

        public ICollection<Valueanswer> Valueanswer { get; set; }
    }
}
