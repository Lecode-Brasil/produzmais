using Domain.Exceptions;

namespace Domain.Validation;
public static class DomainValidation
{
    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength)
        {
            throw new EntityValidationException($"{fieldName} deve ter no máximo {maxLength} caracteres");
        }
    }

    public static void NotNull(object? target, string fieldName)
    {
        if (target is null)
        {
            throw new EntityValidationException($"{fieldName} não pode ser nulo");
        }
    }

    public static void NotNullOrWhiteSpace(string? target, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(target))
        {
            throw new EntityValidationException($"{fieldName} não pode ser nulo, vazio ou apenas espaços");
        }
    }
}
