using FluentValidation.Validators;
using Flunt.Br.Extensions;
using Flunt.Validations;

namespace FluentValidationBR.Extensions
{
    public class CnpjValidator : PropertyValidator
    {
        private readonly Contract contract;

        public CnpjValidator()
        {
            contract = new Contract();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !contract.IsCnpj(value, string.Empty, string.Empty).Valid)
            {
                context.MessageFormatter.AppendArgument(nameof(CnpjValidator), value);
                return false;
            }

            return true;
        }
    }
}
