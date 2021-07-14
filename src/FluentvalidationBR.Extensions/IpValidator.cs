using System.Net;
using FluentValidation;
using FluentValidation.Validators;

namespace FluentValidationBR.Extensions
{
    public class IpValidator<T>: PropertyValidator<T, string>
    {
        public override string Name => nameof(IpValidator<T>);

        public IpValidator()
        {
        }

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é um IP válido.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value != null && !IPAddress.TryParse(value, out var _))
            {
                context.MessageFormatter.AppendArgument(nameof(IpValidator<T>), value);
                return false;
            }

            return true;
        }
    }
}