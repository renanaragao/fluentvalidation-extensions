using FluentValidation;
using FluentValidation.Validators;
using Flunt.Extensions.Br.Validations;
using System.Text.RegularExpressions;

namespace FluentValidationBR.Extensions
{
    public class CepValidator<T> : PropertyValidator<T, string>
    {

        public override string Name => nameof(CepValidator<T>);

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é um CEP válido.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value == null)
                return true;

            value = Regex.Replace(value, @"\D", "");

            if (!Regex.Match(value, @"^\d{8}$").Success)
            {
                context.MessageFormatter.AppendArgument(nameof(CepValidator<T>), value);
                return false;
            }

            return true;
        }
    }
}