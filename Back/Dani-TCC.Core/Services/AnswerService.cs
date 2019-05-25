using System.Collections.Generic;
using System.Linq;
using Dani_TCC.Core.Models;

namespace Dani_TCC.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly DB_PESQUISA_TCCContext _context;
        private readonly IPhotoService _photoService;

        public AnswerService(DB_PESQUISA_TCCContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }
        
        public ICollection<Answer> GenerateAnswers()
        {
            Question question = _context.Question.First();

            int total = _photoService.ListValidSurveyPhotos().Count();

            var result = new List<Answer>();
            for (int i = 0; i < total / Constants.TotalOptions; i++)
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