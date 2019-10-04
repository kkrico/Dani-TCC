using System;
using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Dani_TCC.Core.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly DB_PESQUISA_TCCContext _context;
        private const string Photos = "PHOTOS_CACHE_KEY";

        public CacheService(IMemoryCache memoryCache, DB_PESQUISA_TCCContext context)
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        private T GetOrAssignment<T>(string key, Func<T> callBack) where T: class
        {
            var item = _memoryCache.Get<T>(key);
            if (item != null) return item;
            
            item = callBack();
            _memoryCache.Set(key, item);

            return item;
        }
        
        public IEnumerable<Photo> GetAllPhotos()
        {
            return GetOrAssignment(Photos, () => _context.Photo.AsNoTracking().ToList());
        }
    }
}