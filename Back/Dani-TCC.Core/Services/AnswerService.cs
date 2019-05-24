using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Models;

namespace Dani_TCC.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly DB_PESQUISA_TCCContext _context;
        private readonly ICacheService _cacheService;
        private readonly IPhotoService _photoService;

        public AnswerService(DB_PESQUISA_TCCContext context, ICacheService cacheService, IPhotoService photoService)
        {
            _context = context;
            _cacheService = cacheService;
            _photoService = photoService;
        }
        
        public ICollection<Answer> GenerateAnswers()
        {
            IEnumerable<Photo> allPhotos = _cacheService.GetAllPhotos().OrderBy(d => d.PhotoName).ToList();
            Question question = _context.Question.First();

            int total = _photoService.ListValidSurveyPhotos().Count();

            var result = new List<Answer>();
            for (int i = 0; i < total; i++)
            {
                var answer = new Answer()
                {
                    IdquestionNavigation = question
                };
                
                result.Add(answer);
            }

            return result;
        }
    }
}