using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Dev.EasyPonto.Extensions
{
    public class DataAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var data = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Data em formato inválido");
            }

            return ValidationResult.Success;
        }

    }
}