using FluentValidation.Validators;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidationBR.Extensions
{
    public class EmailsValidator<T> : PropertyValidator<T, string>
    {
        private IEnumerable<string> emailsInvalidos;

        public override string Name => nameof(EmailsValidator<T>);

        protected override string GetDefaultMessageTemplate(string erroCode)
        => $@"'{{PropertyName}}' contém o(s) seguinte(s) e-mail(s) inválido(s): {RetornarEmailsInvalidosConcatenados()}.";

        private string RetornarEmailsInvalidosConcatenados()
        => emailsInvalidos.Select(x => $@"'{x}'")
            .Aggregate((current, next) => $@"{current}, {next}");

        public override bool IsValid(FluentValidation.ValidationContext<T> context, string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            emailsInvalidos = value.Split(';')
                .Where(x => !string.IsNullOrEmpty(x) && !Activator.CreateInstance<Contract<T>>().IsEmail(x, string.Empty).IsValid)
                .ToList();

            if (!emailsInvalidos.Any()) return true;

            return false;
        }
    }
}
