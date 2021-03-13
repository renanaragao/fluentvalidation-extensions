using System.Net;
using FluentValidation.Validators;

namespace FluentvalidationBR.Extensions
{
    public class IpValidator: PropertyValidator
    {

        public IpValidator()
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !IPAddress.TryParse(value, out var _))
            {
                context.MessageFormatter.AppendArgument(nameof(IpValidator), value);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate()
        => @"'{PropertyName}' não é um IP válido.";
    }
}