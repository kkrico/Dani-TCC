using System.ComponentModel;

namespace Dani_TCC.Core.Models
{
    public enum Etnia
    {
        [Description("Branca (o)")]
        Branca = 1,
        [Description("Parda (o)")]
        Parda = 2,
        [Description("Negra (o)")]
        Negra = 3,
        [Description("Indígena (o)")]
        Indigena = 4,
        [Description("Outra (o)")]
        Outra = 5
    }
}