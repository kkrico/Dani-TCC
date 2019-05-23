using System.ComponentModel;

namespace Dani_TCC.Core.Models.Enums
{
    public enum FamilyIncome
    {
        [Description("Até 1 Salário")]
        AtLeast1Salary = 1,
        [Description("Entre 1  e 3 Salários")]
        Between1And3Salaries = 2,
        [Description("Entre 4 e 7 Salários")]
        Between4And7Salaries = 3,
        [Description("Entre 8 e 11 Salários")]
        Between8And11Salaries = 4,
        [Description("Entre 12 e 14 Salários")]
        Between12And14Salaries = 5,
        [Description("Mais de 15 Salários")]
        MoreThan15 = 6
    }
}