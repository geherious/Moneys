using System.ComponentModel.DataAnnotations;

namespace Moneys.Domain.Entities;

public record LoginInfo
{
    [Required]
    public required string AccessToken { get; init; }

    [Required]
    public required RefreshToken RefreshToken { get; init; }
}
