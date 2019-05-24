using System.Collections.Generic;
using Dani_TCC.Core.Models;

namespace Dani_TCC.Core.Services
{
    public interface ICacheService
    {
        IEnumerable<Photo> GetAllPhotos();
    }
}