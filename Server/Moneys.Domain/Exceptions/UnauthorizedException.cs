namespace Moneys.Domain.Exceptions;

public class UnauthenticatedException : Exception
{
    private const string message = "Bad credentials";

    public UnauthenticatedException() : base(message)
    {
    }
}
