using System.Collections.Generic;

namespace Dani_TCC.Core.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public int Idquestion { get; set; }
        public string Questiondescription { get; set; }

        public ICollection<Answer> Answer { get; set; }
    }
}
