using FluentValidation;
using FluentValidation.Results;

namespace Moneys.Domain.Exceptions;

public class NotFoundException : ValidationException
{
    public NotFoundException(string fieldName) : base(Error(fieldName))
    {
    }

    private static ValidationFailure[] Error(string fieldName)
    {
        return new[]
        {
            new ValidationFailure(fieldName, "There is no data with this value")
        };
    }
}
