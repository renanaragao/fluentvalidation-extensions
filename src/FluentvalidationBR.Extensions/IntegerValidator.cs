using FluentValidation;
using FluentValidation.Validators;

namespace FluentValidationBR.Extensions
{
    public class IntegerValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => nameof(IntegerValidator<T>);

        public IntegerValidator()
        {
        }

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é um número válido.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value != null && !int.TryParse(value, out var _))
            {
                context.MessageFormatter.AppendArgument(nameof(IntegerValidator<T>), value);
                return false;
            }

            return true;
        }
    }
}
