using System.Collections.Generic;

namespace Dani_TCC.Core.Models
{
    public partial class Foto
    {
        public Foto()
        {
            Valoresresposta = new HashSet<Valoresresposta>();
        }

        public int Idfoto { get; set; }
        public int? Idgenero { get; set; }
        public int? Idetnia { get; set; }
        public string Hashfoto { get; set; }
        public byte Eleito { get; set; }

        public ICollection<Valoresresposta> Valoresresposta { get; set; }
    }
}
