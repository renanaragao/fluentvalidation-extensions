using FluentValidation.Validators;
using Flunt.Br.Extensions;
using Flunt.Validations;

namespace FluentvalidationBR.Extensions
{
    public class PhoneValidator : PropertyValidator
    {

        private readonly Contract contract;

        public PhoneValidator()
        {
            contract = new Contract();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !contract.IsPhone(value, string.Empty, string.Empty).Valid)
            {
                context.MessageFormatter.AppendArgument(nameof(PhoneValidator), value);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate()
        => @"'{PropertyName}' não é um Telefone válido.";
    }
}