using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dani_TCC.Core.GuardClause;
using Dani_TCC.Core.Models.Enums;

namespace Dani_TCC.Core.ViewModels
{
    public class RegisterSurveyViewModel : IValidatableObject
    {
        public int? Ethnicity { get; set; }
        public int? Gender { get; set; }
        public int? AgeGroup { get; set; }
        public int? FamilyIncome { get; set; }
        public int? Sexuality { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            Validate<Ethnicity>(Ethnicity, result);
            Validate<Gender>(Ethnicity, result);
            Validate<FamilyIncome>(Ethnicity, result);
            Validate<Sexuality>(Ethnicity, result);

            return result;
        }

        private void Validate<T>(int? enumAsInt, ICollection<ValidationResult> results)
        {
            Guard.IsNotNull(results, nameof(results));
            
            ValidationResult validationResult = IsNullOrDefinedOnEnum<T>(enumAsInt);
            if (validationResult != null)
                results.Add(validationResult);
        }

        private ValidationResult IsNullOrDefinedOnEnum<T>(int? enumAsInt)
        {
            if (enumAsInt == null)
                return null;
            bool isDefined = Enum.IsDefined(typeof(T), enumAsInt);
            return !isDefined ? new ValidationResult("Invalid "+typeof(T).Name, new[]{typeof(T).Name}) : null;
        }
    }
    
    public class EndSurveyViewModel
    {
        public int ValueAnswerId { get; set; }
        public int InterVal { get; set; }
    }
}