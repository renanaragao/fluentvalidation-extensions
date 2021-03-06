using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class CepValidatorTests
    {
        private readonly AbstractValidator<CepTest> validator;

        public CepValidatorTests()
        {
            validator = new CepValidatorTest();
        }

        [Theory]
        [InlineData("145698")]
        [InlineData("123456789124")]
        [InlineData("39223797889")]
        [InlineData("")]
        [InlineData(" ")]
        public void Must_Is_InValid(string cep)
        {
            var result = validator.Validate(new CepTest { Cep = cep });

            Assert.False(result.IsValid);
            Assert.Equal("'Cep' não é um CEP válido.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(CepValidator<CepTest>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("07940-200")]
        [InlineData("07.940-200")]
        [InlineData("07940200")]
        [InlineData(null)]
        public void Must_Is_Valid(string cep)
        {
            var result = validator.TestValidate(new CepTest { Cep = cep });

            result.ShouldNotHaveValidationErrorFor(x => x.Cep);
        }
    }

    class CepTest
    {
        public string Cep { get; set; }
    }

    class CepValidatorTest : AbstractValidator<CepTest>
    {
        public CepValidatorTest()
        {
            RuleFor(x => x.Cep).Cep();
        }
    }
}