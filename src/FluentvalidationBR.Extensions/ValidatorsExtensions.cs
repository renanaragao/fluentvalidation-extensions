using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentValidationBR.Extensions
{
    public static class ValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> Integer<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new IntegerValidator<T>());
        public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CpfValidator<T>());
        public static IRuleBuilderOptions<T, string> Cnpj<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CnpjValidator<T>());
        public static IRuleBuilderOptions<T, string> Uri<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new UriValidator<T>());
        public static IRuleBuilderOptions<T, string> CellPhone<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CellPhoneValidator<T>());
        public static IRuleBuilderOptions<T, string> Cep<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new CepValidator<T>());
        public static IRuleBuilderOptions<T, string> Phone<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new PhoneValidator<T>());
        public static IRuleBuilderOptions<T, string> Ip<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new IpValidator<T>());
        public static IRuleBuilderOptions<T, IEnumerable<TProperty>> IsUnique<T, TProperty, TKey>(this IRuleBuilder<T, IEnumerable<TProperty>> ruleBuilder, Expression<Func<TProperty, TKey>> keySelector)
            => ruleBuilder.SetValidator(new IsUniqueValidator<T, TProperty, TKey>(keySelector));
        public static IRuleBuilderOptions<T, IEnumerable<TProperty>> IsUnique<T, TProperty, TKey>(this IRuleBuilder<T, IEnumerable<TProperty>> ruleBuilder, Expression<Func<T, IEnumerable<TProperty>>> expression, Expression<Func<TProperty, TKey>> keySelector)
            => ruleBuilder.SetValidator(new IsUniqueValidator<T, TProperty, TKey>(expression, keySelector));
        public static IRuleBuilderOptions<T, IEnumerable<TProperty>> IsUnique<T, TProperty>(this IRuleBuilder<T, IEnumerable<TProperty>> ruleBuilder)
            => ruleBuilder.SetValidator(new IsUniqueValidator<T, TProperty>());
        /// <summary>
        /// Validate one or more separate emails with ';'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> Emails<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new EmailsValidator<T>());
    }
}
