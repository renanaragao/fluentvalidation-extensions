using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class IpValidatorTests
    {
        private readonly AbstractValidator<IpTest> validator;

        public IpValidatorTests()
        {
            validator = new IpValidatorTest();
        }

        [Theory]
        [InlineData("34.571.585/0001-45")]
        [InlineData("34571585000145")]
        [InlineData("39223797889")]
        [InlineData("300.333.333.3")]
        [InlineData("")]
        [InlineData(" ")]
        public void Must_Is_InValid(string ip)
        {
            var result = validator.Validate(new IpTest { Ip = ip });

            Assert.False(result.IsValid);
            Assert.Equal("'Ip' não é um IP válido.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(IpValidator<IpTest>), result.Errors[0].ErrorCode);
        }

        [Theory]
        [InlineData("255.255.255.255")]
        [InlineData("0.0.0.0")]
        [InlineData("127.0.0.1")]
        [InlineData(null)]
        public void Must_Is_Valid(string ip)
        {
            var result = validator.TestValidate(new IpTest { Ip = ip });

            result.ShouldNotHaveValidationErrorFor(x => x.Ip);
        }
    }

    class IpTest
    {
        public string Ip { get; set; }
    }

    class IpValidatorTest : AbstractValidator<IpTest>
    {
        public IpValidatorTest()
        {
            RuleFor(x => x.Ip).Ip();
        }
    }
}