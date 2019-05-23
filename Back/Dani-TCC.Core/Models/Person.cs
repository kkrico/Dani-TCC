using System.Collections.Generic;

namespace Dani_TCC.Core.Models
{
    public partial class Person
    {
        public Person()
        {
            Survey = new HashSet<Survey>();
        }

        public int Idperson { get; set; }
        public int? Idagegroup { get; set; }
        public int? Idgender { get; set; }
        public int? Idethnicity { get; set; }
        public int? Idsexuality { get; set; }
        public int? Idfamilyincome { get; set; }

        public ICollection<Survey> Survey { get; set; }
    }
}
