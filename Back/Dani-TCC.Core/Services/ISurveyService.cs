using System.Collections.Generic;
using Dani_TCC.Core.ViewModels;

namespace Dani_TCC.Core.Services
{
    public interface ISurveyService
    {
        BeginSurveyViewModel RegisterSurvey(RegisterSurveyViewModel model);
        void EndSurvey(IEnumerable<EndSurveyViewModel> endSurvey);
    }
}