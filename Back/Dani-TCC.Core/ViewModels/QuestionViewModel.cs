using System.Collections.Generic;

namespace Dani_TCC.Core.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionViewModel()
        {
           Options = new List<OptionViewModel>();
        }

        public int AnswerId { get; set; }
        public ICollection<OptionViewModel> Options { get; set; }
    }
}