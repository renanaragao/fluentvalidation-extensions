using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class CpfValidatorTests
    {
        private readonly AbstractValidator<CpfTest> validator;

        public CpfValidatorTests()
        {
            validator = new CpfValidatorTest();
        }

        [Theory]
        [InlineData("145698")]
        [InlineData("123456789124")]
        [InlineData("39223797889")]
        [InlineData("")]
        [InlineData(" ")]
        public void Must_Is_InValid(string cpf)
        {
            var result = validator.Validate(new CpfTest { CPF = cpf });

            Assert.False(result.IsValid);
            Assert.Equal("'CPF' não é um CPF válido.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(CpfValidator<CpfTest>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("660.587.210-08")]
        [InlineData("66058721008")]
        [InlineData(null)]
        public void Must_Is_Valid(string cpf)
        {
            var result = validator.TestValidate(new CpfTest { CPF = cpf });

            result.ShouldNotHaveValidationErrorFor(x => x.CPF);
        }
    }

    class CpfValidatorTest : AbstractValidator<CpfTest>
    {
        public CpfValidatorTest()
        {
            RuleFor(x => x.CPF).Cpf();
        }
    }

    class CpfTest
    {
        public string CPF { get; set; }
    }
}
