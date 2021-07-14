using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class CellPhoneValidatorTests
    {
        private readonly AbstractValidator<CellPhoneTest> validator;

        public CellPhoneValidatorTests()
        {
            validator = new CellPhoneValidatorTest();
        }

        [Theory]
        [InlineData("145698")]
        [InlineData("12345678911")]
        [InlineData("1234567891")]
        [InlineData("+55 12 3456-7891")]
        [InlineData("+551234567891")]
        [InlineData("")]
        [InlineData(" ")]
        public void Must_CellPhone_Is_InValid(string cellPhone)
        {
            var result = validator.Validate(new CellPhoneTest { CellPhone = cellPhone });

            Assert.False(result.IsValid);
            Assert.Equal("'Cell Phone' não é um número de celular válida.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(CellPhoneValidator<CellPhoneTest>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("11 9 9658-7898")]
        [InlineData("+55 11 9 9658-7898")]
        [InlineData("+5511996587898")]
        [InlineData("+55 (11) 9 96587-898")]
        [InlineData(null)]
        public void Must_CellPhone_Is_Valid(string cellPhone)
        {
            var result = validator.TestValidate(new CellPhoneTest { CellPhone = cellPhone });

            result.ShouldNotHaveValidationErrorFor(x => x.CellPhone);
        }
    }

    class CellPhoneValidatorTest : AbstractValidator<CellPhoneTest>
    {
        public CellPhoneValidatorTest()
        {
            RuleFor(x => x.CellPhone).CellPhone();
        }
    }

    class CellPhoneTest
    {
        public string CellPhone { get; set; }
    }
}
