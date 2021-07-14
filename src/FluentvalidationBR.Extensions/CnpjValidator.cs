using FluentValidation;
using FluentValidation.Validators;
using Flunt.Br.Extensions;
using Flunt.Extensions.Br.Validations;
using Flunt.Validations;
using System.Text.RegularExpressions;

namespace FluentValidationBR.Extensions
{
    public class CnpjValidator<T> : PropertyValidator<T, string>
    {
        private readonly Contract<T> contract;

        public override string Name => nameof(CnpjValidator<T>);

        public CnpjValidator()
        {
            contract = new Contract<T>();
        }

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é um CNPJ válido.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {

            if (value == null)
                return true;

            value = Regex.Replace(value, @"\D", "");

            if (!contract.IsCnpj(value, string.Empty).IsValid)
            {
                context.MessageFormatter.AppendArgument(nameof(CnpjValidator<T>), value);
                return false;
            }

            return true;
        }
    }
}
