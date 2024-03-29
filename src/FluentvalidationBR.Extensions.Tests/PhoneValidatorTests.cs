using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class PhoneValidatorTests
    {
        private readonly AbstractValidator<PhoneTest> validator;

        public PhoneValidatorTests()
        {
            validator = new PhoneValidatorTest();
        }

        [Theory]
        [InlineData("145698")]
        [InlineData("123456789124")]
        [InlineData("39223797889")]
        [InlineData("+55 21 99586-8978")]
        [InlineData("+5521995868978")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("3fsdfqaf34afagsbfh")]
        public void Must_Is_InValid(string phone)
        {
            var result = validator.Validate(new PhoneTest { Phone = phone });

            Assert.False(result.IsValid);
            Assert.Equal("'Phone' não é um Telefone válido.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(PhoneValidator<PhoneTest>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("1144891626")]
        [InlineData("11 4489-1626")]
        [InlineData("+552145868978")]
        [InlineData("+55 21 4586-8978")]
        [InlineData(null)]
        public void Must_Is_Valid(string phone)
        {
            var result = validator.TestValidate(new PhoneTest { Phone = phone });

            result.ShouldNotHaveValidationErrorFor(x => x.Phone);
        }
    }

    class PhoneValidatorTest : FluentValidation.AbstractValidator<PhoneTest>
    {
        public PhoneValidatorTest()
        {
            RuleFor(x => x.Phone).Phone();
        }
    }

    class PhoneTest
    {
        public string Phone { get; set; }
    }
}