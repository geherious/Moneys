using Moneys.Domain.Entities;

namespace Moneys.Domain.UseCases.Auth;

public record RefreshUserTokenCommand
{
    public required string Hash { get; init; }
}

public interface IRefreshUserTokenHandler
{
    Task<LoginInfo?> Handle(RefreshUserTokenCommand command);
}
