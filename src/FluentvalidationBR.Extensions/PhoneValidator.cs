using FluentValidation;
using FluentValidation.Validators;
using PhoneNumbers;

namespace FluentValidationBR.Extensions
{
    public class PhoneValidator<T> : PropertyValidator<T, string>
    {

        private readonly PhoneNumberUtil phoneNumberUtil;

        public override string Name => nameof(PhoneValidator<T>);

        public PhoneValidator()
        {
            phoneNumberUtil = PhoneNumberUtil.GetInstance();
        }

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não é um Telefone válido.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (Valid(value))
                return true;

            context.MessageFormatter.AppendArgument(nameof(PhoneValidator<T>), value);
            return false;
        }

        private bool Valid(string value)
        {
            if (value == null)
                return true;

            if (string.Empty == value.Trim())
                return false;

            var phoneNumber = phoneNumberUtil.Parse(value, "BR");

            if (!phoneNumberUtil.IsValidNumber(phoneNumber))
                return false;

            var phoneNumberType = phoneNumberUtil.GetNumberType(phoneNumber);

            if (phoneNumberType == PhoneNumberType.FIXED_LINE)
                return true;

            return false;
        }
    }
}