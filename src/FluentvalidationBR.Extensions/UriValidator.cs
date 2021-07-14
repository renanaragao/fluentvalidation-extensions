using FluentValidation;
using FluentValidation.Validators;
using System;

namespace FluentValidationBR.Extensions
{
    public class UriValidator<T> : PropertyValidator<T, string>
    {
        public override string Name => nameof(UriValidator<T>);

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é uma URI válida.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (value != null && !Uri.TryCreate(value, UriKind.Absolute, out _))
            {
                context.MessageFormatter.AppendArgument(nameof(UriValidator<T>), value);
                return false;
            }

            return true;
        }
    }
}
