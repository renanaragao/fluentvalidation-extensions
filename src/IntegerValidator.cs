using FluentValidation.Validators;

namespace FluentValidation.Extensions
{
    internal class IntegerValidator : PropertyValidator
    {
        public IntegerValidator()
            : base("INTEGER_VALIDATOR_MESSAGE")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !int.TryParse(value, out var _))
            {
                context.MessageFormatter.AppendArgument(nameof(IntegerValidator), value);
                return false;
            }

            return true;
        }
    }
}
