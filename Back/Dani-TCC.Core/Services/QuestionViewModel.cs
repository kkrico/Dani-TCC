using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

namespace Dani_TCC.Core.Services
{
    public class QuestionViewModel
    {
        public int AnswerId { get; set; }
        public ICollection<OptionViewModel> Options { get; set; }
    }

    public class OptionViewModel
    {
        public string Base64Photo { get; set; }
        public int PhotoId { get; set; }
    }
}