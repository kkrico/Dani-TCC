using System.Collections.Generic;

namespace Dani_TCC.Core.Model
{
    public partial class Questao
    {
        public Questao()
        {
            Resposta = new HashSet<Resposta>();
        }

        public int Idquestao { get; set; }
        public string Descricaoquestao { get; set; }

        public ICollection<Resposta> Resposta { get; set; }
    }
}
