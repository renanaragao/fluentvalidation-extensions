using FluentValidation.Validators;
using Flunt.Br.Extensions;
using Flunt.Validations;

namespace FluentValidationBR.Extensions
{
    internal class CpfValidator : PropertyValidator
    {

        private readonly Contract contract;

        public CpfValidator()
        {
            contract = new Contract();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !contract.IsCpf(value, string.Empty, string.Empty).Valid)
            {
                context.MessageFormatter.AppendArgument(nameof(CpfValidator), value);
                return false;
            }

            return true;
        }
    }
}
