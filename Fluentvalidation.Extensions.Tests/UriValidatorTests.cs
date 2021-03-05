using FluentValidation;
using FluentValidation.Extensions;
using Xunit;

namespace Fluentvalidation.Extensions.Tests
{
    public class UriValidatorTests
    {
        private readonly AbstractValidator<UriTest> validator;

        public UriValidatorTests()
        {
            validator = new UriValidatorTest();
        }


        [Theory]
        [InlineData("https//testarrr")]
        [InlineData("httptestarrr")]
        [InlineData("https:/testarrr")]
        [InlineData("https:testarrr")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Must_Uri_Is_InValid(string uri)
        {
            var result = validator.Validate(new UriTest { Uri = uri });

            Assert.False(result.IsValid);
            Assert.Equal("'Uri' não é uma URI válida.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(UriValidator), result.Errors[0].ErrorCode);
        }
    }

    class UriValidatorTest : AbstractValidator<UriTest>
    {
        public UriValidatorTest()
        {
            RuleFor(x => x.Uri).Uri();
        }
    }

    class UriTest
    {
        public string Uri { get; set; }
    }
}
