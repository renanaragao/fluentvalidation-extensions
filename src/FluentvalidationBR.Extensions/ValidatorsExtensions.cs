using FluentValidation;
using FluentValidationBR.Extensions;

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
        /// <summary>
        /// Validate one or more separate emails with ';'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> Emails<T>(this IRuleBuilder<T, string> ruleBuilder) => ruleBuilder.SetValidator(new EmailsValidator<T>());
    }
}
