using Dani_TCC.Core.Models;
using Dani_TCC.Core.Models.Algorithm;

namespace Dani_TCC.Core.Services
{
    public class PhotoService: IPhotoService
    {
        private readonly DB_PESQUISA_TCCContext _context;
        private readonly IEntitySearchAlgorithm<Photo> _entitySearchAlgorithm;

        public PhotoService(DB_PESQUISA_TCCContext context, IEntitySearchAlgorithm<Photo> entitySearchAlgorithm)
        {
            _context = context;
            _entitySearchAlgorithm = entitySearchAlgorithm;
        }
        
        public void ParsePhotos(string folder)
        {
            var photos = _entitySearchAlgorithm.ListEntities(folder);
        }
    }
}