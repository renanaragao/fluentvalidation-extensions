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
        [InlineData("123456789124")]
        [InlineData("")]
        [InlineData(" ")]
        public void Must_CellPhone_Is_InValid(string cellPhone)
        {
            var result = validator.Validate(new CellPhoneTest { CellPhone = cellPhone });

            Assert.False(result.IsValid);
            Assert.Equal("'Cell Phone' não é um número de celular válida.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(CellPhoneValidator), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("11996587898")]
        [InlineData("(11) 9 9658-7898")]
        [InlineData(null)]
        public void Must_CellPhone_Is_Valid(string cellPhone)
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.CellPhone, cellPhone);
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
