using System.ComponentModel.DataAnnotations;

namespace Moneys.Models;

public record LoginUserRequest
{
    [Required]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
