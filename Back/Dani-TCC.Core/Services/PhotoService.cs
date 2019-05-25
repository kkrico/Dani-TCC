using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Models;
using Dani_TCC.Core.Models.Algorithm;

namespace Dani_TCC.Core.Services
{
    public class PhotoService: IPhotoService
    {
        private readonly DB_PESQUISA_TCCContext _context;
        private readonly IEntitySearchAlgorithm<Photo> _entitySearchAlgorithm;
        private readonly ICacheService _cacheService;

        public PhotoService(DB_PESQUISA_TCCContext context, IEntitySearchAlgorithm<Photo> entitySearchAlgorithm, ICacheService cacheService)
        {
            _context = context;
            _entitySearchAlgorithm = entitySearchAlgorithm;
            _cacheService = cacheService;
        }
        
        public void ParsePhotos(string folder)
        {
            IEnumerable<Photo> currentPhotos = _entitySearchAlgorithm.ListEntities(folder);
            
            
            IEnumerable<string> existingPhotoHashes = _context.Photo.Select(photo => photo.Photohash).ToList();
            IEnumerable<string> currentPhotoHashes = currentPhotos.Select(e => e.Photohash);
            
            AddNewPhotos(currentPhotoHashes, existingPhotoHashes, currentPhotos);
            DeleteNonUsedPhotos(existingPhotoHashes, currentPhotoHashes);

            _context.SaveChanges();
        }

        public IEnumerable<Photo> ListValidSurveyPhotos()
        {
            List<Photo> allPhotos = _cacheService.GetAllPhotos().OrderBy(d => d.PhotoName).ToList();
            
            int total = allPhotos.Count();

            while (total % Constants.TotalOptions != 0)
            {
                total = total - 1;
            }
            
            return allPhotos.Take(total);
        }

        private void AddNewPhotos(IEnumerable<string> currentPhotoHashes, IEnumerable<string> existingPhotoHashes, IEnumerable<Photo> currentPhotos)
        {
            IEnumerable<string> hashesToAdd = currentPhotoHashes.Where(cp => !existingPhotoHashes.Contains(cp));
            foreach (string hashToAdd in hashesToAdd)
            {
                Photo newPhoto = currentPhotos.First(d => d.Photohash == hashToAdd);
                _context.Add(newPhoto);
            }
        }

        private void DeleteNonUsedPhotos(IEnumerable<string> existingPhotoHashes, IEnumerable<string> currentPhotoHashes)
        {
            IEnumerable<string> hashesToDelete = existingPhotoHashes.Except(currentPhotoHashes);
            IQueryable<Photo> photosToDelete = _context.Photo.Where(p => hashesToDelete.Contains(p.Photohash));
            _context.Photo.RemoveRange(photosToDelete);
        }
    }
}