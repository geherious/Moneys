using Moneys.Domain.Entities;

namespace Moneys.Domain.UseCases.Auth;

public record LoginUserCommand
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}

public interface ILoginUserHandler
{
    Task<LoginInfo?> Handle(LoginUserCommand command);
}
