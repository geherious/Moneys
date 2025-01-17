using System.ComponentModel.DataAnnotations;

namespace Moneys.Models;

public record RegisterUserRequest
{
    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Email { get; init; }
    
    [Required]
    public required string Password { get; init; }
}
