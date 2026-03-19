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
        [InlineData("ER.HSD.YUE/JHFGG-65")]
        [InlineData("ER.HSD.YUE/0001-65")]
        [InlineData("0B.C2V.3C9/7V23-22")]
        [InlineData("ER.HSD/JHFGG-65")]
        public void Must_Is_InValid(string cnpj)
        {
            var result = validator.Validate(new CnpjTest { CNPJ = cnpj });

            Assert.False(result.IsValid);
            Assert.Equal("'CNPJ' não é um CNPJ válido.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(CnpjValidator<>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("34.571.585/0001-11")]
        [InlineData("34571585000111")]
        [InlineData("0B.C2V.3B9/7V23-22")]
        [InlineData("0BC2V3B97V2322")]
        [InlineData("0B.C2V.3B9/0001-18")]
        [InlineData("0B.C2V.3B9/7V23-22")]
        [InlineData("0BC2V3B9000118")]
        [InlineData("0bc2v3b9000118")]
        [InlineData(null)]
        public void Must_Is_Valid(string cnpj)
        {
            var result = validator.TestValidate(new CnpjTest { CNPJ = cnpj });

            result.ShouldNotHaveValidationErrorFor(x => x.CNPJ);
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
