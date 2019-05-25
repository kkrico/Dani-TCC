using System;
using System.Collections.Generic;

namespace Dani_TCC.Core.ViewModels
{
    public class BeginSurveyViewModel
    {
        public BeginSurveyViewModel()
        {
            Questions = new List<QuestionViewModel>();
        }

        public string SurveyCommand { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}