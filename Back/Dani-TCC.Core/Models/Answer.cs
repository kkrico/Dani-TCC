using System.Collections.Generic;

namespace Dani_TCC.Core.Models
{
    public partial class Answer
    {
        public Answer()
        {
            Valueanswer = new HashSet<Valueanswer>();
        }

        public int Idanswer { get; set; }
        public int Idsurvey { get; set; }
        public int Idquestion { get; set; }

        public Question IdquestionNavigation { get; set; }
        public Survey IdsurveyNavigation { get; set; }
        public ICollection<Valueanswer> Valueanswer { get; set; }
    }
}
