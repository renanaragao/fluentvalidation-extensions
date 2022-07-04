using FluentValidation;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FluentValidationBR.Extensions.Tests
{
    public class IsUniqueValidatorTests
    {
        [Fact]
        public void Must_Fail_When_Validating_Single_Complex_Type()
        {

            var result = new IsUniqueValidatorTest().TestValidate(new DocumentTest
            {
                ItemsA = new[] {
                    new ItemDocumentTest
                    {
                        Id = "2413",
                    },
                    new ItemDocumentTest
                    {
                        Id = "2413"
                    }
                }
            });

            Assert.False(result.IsValid);
            Assert.Equal("'Id' deve ser único.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(IsUniqueValidator<DocumentTest, ItemDocumentTest, string>), result.Errors[0].ErrorCode);
        }
        [Fact]
        public void Must_Validate_Duplicates_Between_Lists()
        {

            var result = new IsUniqueValidatorTest().TestValidate(new DocumentTest
            {
                ItemsA = new[] {
                    new ItemDocumentTest
                    {
                        Id = "43534",
                    },
                    new ItemDocumentTest
                    {
                        Id = "2413"
                    }
                },
                ItemsB = new[] {
                    new ItemDocumentTest
                    {
                        Id = "63532",
                    },
                    new ItemDocumentTest
                    {
                        Id = "2413"
                    }
                }
            });

            Assert.False(result.IsValid);
            Assert.Equal("Existem elementos com a propriedade 'Id' igual entre a lista 'Items A' e 'Items B'.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(IsUniqueValidator<DocumentTest, ItemDocumentTest, string>), result.Errors[0].ErrorCode);
        }

        [Theory, MemberData(nameof(GetValidItems))]
        public void Not_Must_Fail_When_Validating_Single_Complex_Type(DocumentTest documentTest)
        {
            var result = new IsUniqueValidatorTest().TestValidate(documentTest);

            result.ShouldNotHaveValidationErrorFor(x => x.ItemsA);
            result.ShouldNotHaveValidationErrorFor(x => x.Codes);
        }

        [Fact]
        public void Must_Fail_When_Validating_Single_Primitive_Type()
        {

            var result = new IsUniqueValidatorTest().TestValidate(new DocumentTest
            {
                Codes = new[] { "2342", "2342" }
            });

            Assert.False(result.IsValid);
            Assert.Equal("'Codes' não deve conter elementos duplicados.", result.Errors[0].ErrorMessage);
            Assert.Equal(nameof(IsUniqueValidator<DocumentTest, string>), result.Errors[0].ErrorCode);
        }

        public static IEnumerable<object[]> GetValidItems()
        {
            yield return new object[] {  new DocumentTest
                {
                    ItemsA = new[] {
                        new ItemDocumentTest
                        {
                            Id = "2413"
                        },
                        new ItemDocumentTest
                        {
                            Id = "425234"
                        }
                    },
                    ItemsB = new[] {
                        new ItemDocumentTest
                        {
                            Id = "8456"
                        },
                        new ItemDocumentTest
                        {
                            Id = "83323"
                        }
                    },
                    Codes = new[] { "2342", "2352344" }
                }
            };

            yield return new object[] { new DocumentTest() };
            yield return new object[] {  new DocumentTest
                {
                    ItemsA = Enumerable.Empty<ItemDocumentTest>(),
                    ItemsB = Enumerable.Empty<ItemDocumentTest>(),
                    Codes = Enumerable.Empty<string>()
                }
            };
        }
    }

    class IsUniqueValidatorTest : AbstractValidator<DocumentTest>
    {
        public IsUniqueValidatorTest()
        {
            RuleFor(x => x.ItemsA).IsUnique(x => x.Id);
            RuleFor(x => x.Codes).IsUnique();
            RuleFor(x => x.ItemsA).IsUnique(x => x.ItemsB, x => x.Id);
        }
    }

    public class DocumentTest
    {
        public IEnumerable<ItemDocumentTest> ItemsA { get; set; }
        public IEnumerable<ItemDocumentTest> ItemsB { get; set; }
        public IEnumerable<string> Codes { get; set; }
    }

    public class ItemDocumentTest
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
