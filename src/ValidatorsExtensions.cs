﻿using FluentValidation;

namespace FluentValidation.Extensions
{
    public static class ValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> Integer<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new IntegerValidator());
        public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CpfValidator());
        public static IRuleBuilderOptions<T, string> Cnpj<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CnpjValidator());
        public static IRuleBuilderOptions<T, string> Uri<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new UriValidator());
    }
}
