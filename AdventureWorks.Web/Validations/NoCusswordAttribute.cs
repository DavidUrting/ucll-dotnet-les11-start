using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdventureWorks.Web.Validations
{
    public class NoCusswordAttribute : ValidationAttribute
    {
        private List<string> _cussword = new List<string>
        {
            "whoopsie",
            "omg",
            "potvolkoffie"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string text = value as string;
            if (text != null && _cussword.Exists(c => c == text.ToLower()))
            {
                return new ValidationResult("Vloeken mag niet");
            }

            return ValidationResult.Success;
        }
    }
}
