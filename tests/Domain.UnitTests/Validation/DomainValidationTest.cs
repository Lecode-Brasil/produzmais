using Bogus;
using Domain.Exceptions;
using Domain.Validation;
using FluentAssertions;

namespace Domain.UnitTests.Validation;
public class DomainValidationTest
{
    private Faker _faker = new();

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
        string? value = null;
        Action action = () => DomainValidation.NotNull(value, "FieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName não pode ser nulo");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void NotNullOrWhiteSpaceThrowWhenIsNullOrWhiteSpace(string? value)
    {
        Action action = () => DomainValidation.NotNullOrWhiteSpace(value, "FieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName não pode ser nulo, vazio ou apenas espaços");
    }

    [Fact]
    public void NotNullOrWhiteSpaceOk()
    {
        string value = _faker.Commerce.ProductName();
        Action action = () => DomainValidation.NotNullOrWhiteSpace(value, nameof(value));

        action.Should().NotThrow();
    }
}
