using System.ComponentModel;

namespace Dani_TCC.Core.Models
{
    public enum FaixaEtaria
    {
        [Description("Entre 18 e 20 anos")]
        Entre18Ate20 = 1,
        [Description("Entre 21 e 25 anos")]
        Entre21Ate25 = 2,
        [Description("Entre 26 e 30 anos")]
        Entre26Ate30 = 3,
        [Description("Entre 31 e 39 anos")]
        Entre31Ate39 = 4,
        [Description("Entre 40 e 49 anos")]
        Entre40Ate49 = 5,
        [Description("Mais de 50 anos")]
        Mais50 = 6
    }
}