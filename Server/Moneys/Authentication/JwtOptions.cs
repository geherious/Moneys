namespace Moneys.ServiceCollectionExtensions;

public record JwtOptions
{
    public required string Issuer { get; init; }

    public required string Audience { get; init; }

    public required string Key { get; init; }
}
