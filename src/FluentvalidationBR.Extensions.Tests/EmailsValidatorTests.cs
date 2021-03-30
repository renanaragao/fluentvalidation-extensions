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
        [InlineData("email@com.br;email@; ;test2", "'email@', 'test2'")]

        public void Must_Is_InValid(string emails, string emailsInvalidos)
        {
            var result = validator.Validate(new EmailsTest { Emails = emails });

            Assert.False(result.IsValid);
            Assert.Equal($"'Emails' contém o(s) seguinte(s) e-mail(s) inválido(s): {emailsInvalidos}.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(EmailsValidator), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("email@com.br;")]
        [InlineData("email@com.br")]
        [InlineData("email@com.br; teste@com.br;email3@com.br")]
        [InlineData("email@com.br; teste@com.br ;email3@com.br")]
        [InlineData("email@com.br;teste@com.br ;email3@com.br")]
        [InlineData("email@com.br;teste@com.br;email3@com.br")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Must_Is_Valid(string emails)
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Emails, emails);
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
