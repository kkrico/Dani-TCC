using System;

namespace Dani_TCC.Core.Models
{
    public partial class Valueanswer
    {
        public int Idvalueanswer { get; set; }
        public int Idanswer { get; set; }
        public int Idphoto { get; set; }
        public byte Haschoosen { get; set; }
        public DateTimeOffset Selectiontime { get; set; }

        public Answer IdanswerNavigation { get; set; }
        public Photo IdphotoNavigation { get; set; }
    }
}
