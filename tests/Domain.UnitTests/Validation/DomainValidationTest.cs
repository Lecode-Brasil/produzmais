using Bogus;
using Domain.Exceptions;
using Domain.Validation;
using FluentAssertions;

namespace Domain.UnitTests.Validation;
public class DomainValidationTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void NotNullOk()
    {
        string value = _faker.Commerce.ProductName();

        Action action = () => DomainValidation.NotNull(value, nameof(value));

        action.Should().NotThrow();
    }

    [Fact]
    public void NotNullThrowWhenNull()
    {
        string? target = null;
        string fieldName = _faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.NotNull(target, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} não pode ser nulo");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void NotNullOrWhiteSpaceThrowWhenIsNullOrWhiteSpace(string? target)
    {
        string fieldName = _faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.NotNullOrWhiteSpace(target, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} não pode ser nulo, vazio ou apenas espaços");
    }

    [Fact]
    public void NotNullOrWhiteSpaceOk()
    {
        string target = _faker.Commerce.ProductName();

        Action action = () => DomainValidation.NotNullOrWhiteSpace(target, nameof(target));

        action.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
    public void MaxLengthThrowWhenGreater(string target, int maxLength)
    {
        string fieldName = _faker.Commerce.ProductName().Replace(" ", "");
        Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} deve ter no máximo {maxLength} caracteres");
    }

    public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberOfTests)
    {
        yield return new object[] { "123456", 5 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example  = faker.Commerce.ProductName();
            var maxLength = example.Length - (new Random()).Next(1, 5);
            yield return new object[] { example, maxLength };
        }
    }

    [Theory]
    [MemberData(nameof(GetValuesMaxLengthOk), parameters: 10)]
    public void MaxLengthOk(string target, int maxLength)
    {
        string fieldName = _faker.Commerce.ProductName().Replace(" ", "");

        Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesMaxLengthOk(int numberOfTests)
    {
        yield return new object[] { "123456", 6 };
        var faker = new Faker();
        for (int i = 0; i < numberOfTests; i++)
        {
            var example = faker.Commerce.ProductName();
            var maxLength = example.Length + (new Random()).Next(1, 5);
            yield return new object[] { example, maxLength };
        }
    }
}
