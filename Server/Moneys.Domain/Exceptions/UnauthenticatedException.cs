namespace Moneys.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    private const string message = "Unauthorized";

    public UnauthorizedException() : base(message)
    {
        
    }
}
