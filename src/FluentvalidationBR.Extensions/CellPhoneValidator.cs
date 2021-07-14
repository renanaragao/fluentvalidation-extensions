using System;
using FluentValidation;
using FluentValidation.Validators;
using Flunt.Extensions.Br.Validations;
using Flunt.Validations;
using PhoneNumbers;

namespace FluentValidationBR.Extensions
{
    public class CellPhoneValidator<T> : PropertyValidator<T, string>
    {

        private readonly PhoneNumberUtil phoneNumberUtil;

        public override string Name => nameof(CellPhoneValidator<T>);

        public CellPhoneValidator()
        {
            phoneNumberUtil = PhoneNumberUtil.GetInstance();
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        => @"'{PropertyName}' não é um número de celular válida.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (Valid(value))
                return true;

            context.MessageFormatter.AppendArgument(nameof(CellPhoneValidator<T>), value);
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

            if (phoneNumberType == PhoneNumberType.MOBILE)
                return true;

            return false;
        }
    }
}
