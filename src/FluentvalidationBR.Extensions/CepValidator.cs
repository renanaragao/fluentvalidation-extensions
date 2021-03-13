using FluentValidation.Validators;
using Flunt.Validations;
using Flunt.Br.Extensions;

namespace FluentvalidationBR.Extensions
{
    public class CepValidator : PropertyValidator
    {

        private readonly Contract contract;

        public CepValidator()
        {
            contract = new Contract();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !contract.IsCep(value, string.Empty, string.Empty).Valid)
            {
                context.MessageFormatter.AppendArgument(nameof(CepValidator), value);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate()
        => @"'{PropertyName}' não é um CEP válido.";
    }
}