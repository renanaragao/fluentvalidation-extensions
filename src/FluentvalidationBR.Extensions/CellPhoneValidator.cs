using FluentValidation.Validators;
using Flunt.Br.Extensions;
using Flunt.Validations;

namespace FluentValidationBR.Extensions
{
    public class CellPhoneValidator : PropertyValidator
    {

        private readonly Contract contract;

        public CellPhoneValidator()
        {
            contract = new Contract();
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !contract.IsCellPhone(value, string.Empty, string.Empty).Valid)
            {
                context.MessageFormatter.AppendArgument(nameof(CellPhoneValidator), value);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate()
        => @"'{PropertyName}' não é um número de celular válida.";
    }
}
