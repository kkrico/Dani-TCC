using System;
using System.Collections.Generic;

namespace Dani_TCC.Core.Model
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Pesquisa = new HashSet<Pesquisa>();
        }

        public int Idpessoa { get; set; }
        public int? Idfaixaetaria { get; set; }
        public int? Idgenero { get; set; }
        public int? Idetnia { get; set; }
        public int? Idsexualidade { get; set; }
        public int? Idrendafamilia { get; set; }
        public string Emailpessoa { get; set; }

        public ICollection<Pesquisa> Pesquisa { get; set; }
    }
}
