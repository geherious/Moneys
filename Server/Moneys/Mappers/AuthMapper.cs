using Moneys.Domain.Entities;
using Moneys.Models;

namespace Moneys.Mappers;

public static class AuthMapper
{
    public static User ToDomain(this RegisterUserRequest request) =>
        new()
        {
            Username = request.Username,
            Email = request.Email,
            RegisteredAt = DateTime.UtcNow
        };
}
