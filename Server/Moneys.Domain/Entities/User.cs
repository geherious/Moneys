namespace Moneys.Domain.Entities;

public record class User
{
    public long Id { get; init; }

    public required string Username { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public required DateTime RegisteredAt { get; init; }
}
