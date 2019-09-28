using System;
using System.Collections.Generic;

namespace Dani_TCC.Core.Models
{
    public partial class Survey
    {
        public Survey()
        {
            Answer = new HashSet<Answer>();
        }

        public int Idsurvey { get; set; }
        public int? Idperson { get; set; }
        public DateTime Initialfilldate { get; set; }
        public DateTime? Finalfilldate { get; set; }
        public Person IdpersonNavigation { get; set; }
        public ICollection<Answer> Answer { get; set; }
    }
}
