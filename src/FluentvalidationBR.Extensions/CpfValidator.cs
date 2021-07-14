using FluentValidation;
using FluentValidation.Validators;
using Flunt.Br.Extensions;
using Flunt.Extensions.Br.Validations;
using Flunt.Validations;
using System.Text.RegularExpressions;

namespace FluentValidationBR.Extensions
{
    public class CpfValidator<T> : PropertyValidator<T, string>
    {

        private readonly Contract<T> contract;

        public override string Name => nameof(CpfValidator<T>);

        public CpfValidator()
        {
            contract = new Contract<T>();
        }

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é um CPF válido.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value == null)
                return true;

            value = Regex.Replace(value, @"\D", "");

            if (!contract.IsCpf(value, string.Empty).IsValid)
            {
                context.MessageFormatter.AppendArgument(nameof(CpfValidator<T>), value);
                return false;
            }

            return true;
        }
    }
}
