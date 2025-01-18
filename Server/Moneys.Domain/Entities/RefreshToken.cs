namespace Moneys.Domain.Entities;

public record RefreshToken
{
    public required long UserId { get; init; }

    public required string Hash { get; init; }

    public required DateTime ExpiresAt { get; init; }
}
