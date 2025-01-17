using FluentValidation;
using FluentValidation.Results;

namespace Moneys.Domain.Exceptions;

public class AlreadyExistsException : ValidationException
{
    public AlreadyExistsException(string fieldName) : base(Error(fieldName))
    {
    }

    private static ValidationFailure[] Error(string fieldName)
    {
        return new[]
        {
            new ValidationFailure(fieldName, "This value already exists")
        };
    }
}
