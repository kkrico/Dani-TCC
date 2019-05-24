using System.Collections.Generic;

namespace Dani_TCC.Core.ViewModels
{
    public class BeginSurveyViewModel
    {
        public BeginSurveyViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }
        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}