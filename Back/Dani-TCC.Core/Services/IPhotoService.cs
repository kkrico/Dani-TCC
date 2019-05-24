using System.Collections.Generic;
using Dani_TCC.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Dani_TCC.Core.Services
{
    public interface IPhotoService
    {
        void ParsePhotos(string folder);

        IEnumerable<Photo> ListValidSurveyPhotos();
    }
}