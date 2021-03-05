using FluentValidation.Validators;

namespace FluentValidationBR.Extensions
{
    public class IntegerValidator : PropertyValidator
    {
        public IntegerValidator()
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

        protected override string GetDefaultMessageTemplate()
        => @"'{PropertyName}' não é um número válido.";
    }
}
