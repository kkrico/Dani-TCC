using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Models;

namespace Dani_TCC.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly DB_PESQUISA_TCCContext _context;
        private readonly ICacheService _cacheService;

        public AnswerService(DB_PESQUISA_TCCContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }
        
        public ICollection<Answer> GenerateAnswers()
        {
            IEnumerable<Photo> allPhotos = _cacheService.GetAllPhotos().OrderBy(d => d.PhotoName).ToList();
            Question question = _context.Question.First();

            int total = allPhotos.Count();
            total = total % 2 != 0 ? total - 1 : total;
            int answerQty = total / 2;

            var result = new List<Answer>();
            for (int i = 0; i < answerQty; i++)
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