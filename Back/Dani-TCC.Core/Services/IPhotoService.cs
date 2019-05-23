using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dani_TCC.Core.Services
{
    public interface IPhotoService
    {
        void ParsePhotos(string folder);
    }
}