using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class IntegerValidatorTests
    {
        private readonly AbstractValidator<IntegerTest> validator;

        public IntegerValidatorTests()
        {
            validator = new IntegerValidatorTest();
        }

        [Theory]
        [InlineData("34.571.585/0001-45")]
        [InlineData("34571585000145")]
        [InlineData("39223797889")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("car")]
        public void Must_Is_InValid(string number)
        {
            var result = validator.Validate(new IntegerTest { Number = number });

            Assert.False(result.IsValid);
            Assert.Equal("'Number' não é um número válido.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(IntegerValidator<IntegerTest>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("34")]
        [InlineData("345715")]
        [InlineData(null)]
        public void Must_Is_Valid(string number)
        {
            var result = validator.TestValidate(new IntegerTest { Number = number });

            result.ShouldNotHaveValidationErrorFor(x => x.Number);
        }
    }

    class IntegerValidatorTest : AbstractValidator<IntegerTest>
    {
        public IntegerValidatorTest()
        {
            RuleFor(x => x.Number).Integer();
        }
    }

    class IntegerTest
    {
        public string Number { get; set; }
    }
}
