using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Dani_TCC.Core.Models.Enums;

namespace Dani_TCC.Core.Models
{
    public class PhotoLocation
    {
        private static readonly SHA256 Sha256; 

        static PhotoLocation()
        {
            Sha256 = SHA256.Create();
        }
        private readonly string _fileLocation;

        public PhotoLocation(string fileLocation)
        {
            _fileLocation = fileLocation ?? throw new ArgumentNullException(nameof(fileLocation));
        }

        public Ethnicity Ethnicity
        {
            get
            {
                if (_fileLocation.Contains("Pret"))
                    return Ethnicity.Black;
                if (_fileLocation.Contains("Bran"))
                    return Ethnicity.White;
                if (_fileLocation.Contains("Pard"))
                    return Ethnicity.GrayishBrown;
                
                throw new InvalidOperationException();
            }
        }

        public Gender Gender => _fileLocation.Contains("Masc") ? Gender.Male : Gender.Female;
        
        public static implicit operator Photo(PhotoLocation v)
        {
            var byteContents = GetFileContents(v._fileLocation);
            return new Photo
            {
                Idgender = (int)v.Gender,
                Idethnicity = (int)v.Ethnicity,
                Elected = Convert.ToByte(!v._fileLocation.Contains("N eleitos")),
                Photohash = BytesToHash(byteContents),
                FileContents = byteContents,
                PhotoName = Path.GetFileNameWithoutExtension(v._fileLocation)
            };
        }

        private static byte[] GetFileContents(string fileLocation)
        {
            using (FileStream stream = File.OpenRead(fileLocation))
            {
                return Sha256.ComputeHash(stream);
            }
        }
        
        private static string BytesToHash(byte[] bytes)
        {
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }
    }
}