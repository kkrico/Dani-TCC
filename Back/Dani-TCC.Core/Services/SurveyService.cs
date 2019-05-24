using Dani_TCC.Core.Models;
using Dani_TCC.Core.ViewModels;

namespace Dani_TCC.Core.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly DB_PESQUISA_TCCContext _context;

        public SurveyService(DB_PESQUISA_TCCContext context)
        {
            _context = context;
        }
        
        public void RegisterSurvey(RegisterSurveyViewModel model)
        {
            
            throw new System.NotImplementedException();
        }
    }
}