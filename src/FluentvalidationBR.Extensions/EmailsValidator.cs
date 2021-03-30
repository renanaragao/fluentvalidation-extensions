using FluentValidation.Validators;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidationBR.Extensions
{
    public class EmailsValidator : PropertyValidator
    {

        private readonly Contract contract;
        private IEnumerable<string> emailsInvalidos;

        public EmailsValidator()
        {
            contract = new Contract();
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (string.IsNullOrWhiteSpace(value))
                return true;

            emailsInvalidos = value.Split(';')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x) && contract.IsEmail(x, string.Empty, string.Empty).Invalid)
                .ToList();

            if (!emailsInvalidos.Any()) return true;

            return false;
        }

        protected override string GetDefaultMessageTemplate()
        => $@"'{{PropertyName}}' contém o(s) seguinte(s) e-mail(s) inválido(s): {RetornarEmailsInvalidosConcatenados()}.";

        private string RetornarEmailsInvalidosConcatenados()
        => emailsInvalidos.Select(x => $@"'{x}'")
            .Aggregate((current, next) => $@"{current}, {next}");
    }
}
