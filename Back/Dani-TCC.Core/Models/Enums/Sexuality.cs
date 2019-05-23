using System.ComponentModel;

namespace Dani_TCC.Core.Models.Enums
{
    public enum Sexuality
    {
        [Description("Hétero")]
        Hetero = 1,
        [Description("Bisexual")]
        Bisexual = 2,
        [Description("Homossexual")]
        Homosexual = 3,
        [Description("Outros")]
        Another = 4
    }
}