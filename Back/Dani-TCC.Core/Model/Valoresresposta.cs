using System;
using System.Collections.Generic;

namespace Dani_TCC.Core.Model
{
    public partial class Valoresresposta
    {
        public int Idvalorresposta { get; set; }
        public int Idresposta { get; set; }
        public int Idfoto { get; set; }
        public byte Foiselecionada { get; set; }
        public DateTimeOffset Temposelecao { get; set; }

        public Foto IdfotoNavigation { get; set; }
        public Resposta IdrespostaNavigation { get; set; }
    }
}
