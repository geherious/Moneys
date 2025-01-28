using Moneys.Domain.UseCases.Auth;
using Moneys.Models;

namespace Moneys.Mappers;

public static class AuthMapper
{
    public static RegisterUserCommand ToCommand(this RegisterUserRequest request) =>
        new()
        {
            Email = request.Email,
            Password = request.Password
        };
    
    public static LoginUserCommand ToCommand(this LoginUserRequest request) =>
    new()
    {
        Email = request.Email,
        Password = request.Password
    };
}
