using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class EmailsValidatorTests
    {
        private readonly AbstractValidator<EmailsTest> validator;

        public EmailsValidatorTests()
        {
            validator = new EmailsValidatorTest();
        }

        [Theory]
        [InlineData("email.com", "'email.com'")]
        [InlineData("email@com.br;email", "'email'")]
        [InlineData("email@com.br;email@teste.com;test", "'test'")]
        [InlineData("email@com.br;email@;test", "'email@', 'test'")]
        [InlineData("email@com.br;email@;test;test2", "'email@', 'test', 'test2'")]
        [InlineData("email@com.br;email@;;test2", "'email@', 'test2'")]
        [InlineData("email@com.br;email@; ;test2", "'email@', ' ', 'test2'")]
        [InlineData("casdadsd;asdasdd;teste@hotmail.com", "'casdadsd', 'asdasdd'")]
        [InlineData("casdadsd;asdasdd;teste@hotmail.com;teste1@hotmail.com", "'casdadsd', 'asdasdd'")]
        [InlineData("casdadsd;teste@hotmail.com;asdasdd;", "'casdadsd', 'asdasdd'")]
        [InlineData("casdadsd;teste@hotmail.com;teste1@hotmail.com;asdasdd;", "'casdadsd', 'asdasdd'")]
        [InlineData(" ", "' '")]
        public void Must_Is_InValid(string emails, string emailsInvalidos)
        {
            var result = validator.Validate(new EmailsTest { Emails = emails });

            Assert.False(result.IsValid);
            Assert.Equal($"'Emails' contém o(s) seguinte(s) e-mail(s) inválido(s): {emailsInvalidos}.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(EmailsValidator<EmailsTest>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("email@com.br;")]
        [InlineData("email@com.br")]
        [InlineData("email@com.br;teste@com.br;email3@com.br")]
        [InlineData("email@com.br;teste@com.br;email3@com.br")]
        [InlineData("email@com.br;teste@com.br;email3@com.br")]
        [InlineData("email@com.br;teste@com.br;email3@com.br")]
        [InlineData(null)]
        [InlineData("")]
        public void Must_Is_Valid(string emails)
        {
            var result = validator.TestValidate(new EmailsTest { Emails = emails });

            result.ShouldNotHaveValidationErrorFor(x => x.Emails);
        }
    }

    class EmailsValidatorTest : AbstractValidator<EmailsTest>
    {
        public EmailsValidatorTest()
        {
            RuleFor(x => x.Emails).Emails();
        }
    }

    class EmailsTest
    {
        public string Emails { get; set; }
    }
}
