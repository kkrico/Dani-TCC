using System.ComponentModel;

namespace Dani_TCC.Core.Models
{
    public enum RendaFamiliar
    {
        [Description("Até 1 Salário")]
        Ate1Salario = 1,
        [Description("Entre 1  e 3 Salários")]
        Entre1E3Salario = 2,
        [Description("Entre 4 e 7 Salários")]
        Entre4E7Salario = 3,
        [Description("Entre 8 e 11 Salários")]
        Entre8E11Salario = 4,
        [Description("Entre 12 e 14 Salários")]
        Entre12E14Salario = 5,
        [Description("Mais de 15 Salários")]
        Mais15 = 6
    }
}