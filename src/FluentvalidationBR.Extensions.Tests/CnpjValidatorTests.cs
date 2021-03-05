using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class CnpjValidatorTests
    {
        private readonly AbstractValidator<CnpjTest> validator;

        public CnpjValidatorTests()
        {
            validator = new CnpjValidatorTest();
        }

        [Theory]
        [InlineData("34.571.585/0001-45")]
        [InlineData("34571585000145")]
        [InlineData("39223797889")]
        [InlineData("")]
        [InlineData(" ")]
        public void Must_Is_InValid(string cnpj)
        {
            var result = validator.Validate(new CnpjTest { CNPJ = cnpj });

            Assert.False(result.IsValid);
            Assert.Equal("'CNPJ' não é um CNPJ válido.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(CnpjValidator), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("34.571.585/0001-11")]
        [InlineData("34571585000111")]
        [InlineData(null)]
        public void Must_Is_Valid(string cnpj)
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.CNPJ, cnpj);
        }
    }

    class CnpjValidatorTest : AbstractValidator<CnpjTest>
    {
        public CnpjValidatorTest()
        {
            RuleFor(x => x.CNPJ).Cnpj();
        }
    }

    class CnpjTest
    {
        public string CNPJ { get; set; }
    }
}
