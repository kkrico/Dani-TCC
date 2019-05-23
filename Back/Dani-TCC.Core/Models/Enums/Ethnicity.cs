using System.ComponentModel;

namespace Dani_TCC.Core.Models.Enums
{
    public enum Ethnicity
    {
        [Description("Branca (o)")]
        White = 1,
        [Description("Parda (o)")]
        GrayishBrown = 2,
        [Description("Negra (o)")]
        Black = 3,
        [Description("Indígena (o)")]
        Indian = 4,
        [Description("Outra (o)")]
        Another = 5
    }
}