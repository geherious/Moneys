namespace Moneys.Domain.UseCases.Auth;

public record RegisterUserCommand
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}

public interface IRegisterUserHandler
{
    Task Handle(RegisterUserCommand command);
}
