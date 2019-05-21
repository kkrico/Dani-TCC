using System.Collections.Generic;

namespace Dani_TCC.Core.Model
{
    public partial class Resposta
    {
        public Resposta()
        {
            Valoresresposta = new HashSet<Valoresresposta>();
        }

        public int Idresposta { get; set; }
        public int Idpesquisa { get; set; }
        public int Idquestao { get; set; }

        public Pesquisa IdpesquisaNavigation { get; set; }
        public Questao IdquestaoNavigation { get; set; }
        public ICollection<Valoresresposta> Valoresresposta { get; set; }
    }
}
