using System;
using System.Collections.Generic;

namespace Dani_TCC.Core.Model
{
    public partial class Pesquisa
    {
        public Pesquisa()
        {
            Resposta = new HashSet<Resposta>();
        }

        public int Idpesquisa { get; set; }
        public int Idpessoa { get; set; }
        public DateTime? Horainiciopreenchimento { get; set; }
        public DateTime? Horafimpreenchimento { get; set; }

        public Pessoa IdpessoaNavigation { get; set; }
        public ICollection<Resposta> Resposta { get; set; }
    }
}
