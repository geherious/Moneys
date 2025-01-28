using System.Security.Claims;
using Moneys.Domain.Entities;
using Moneys.Domain.Repositories;
using Moneys.Domain.Services;

namespace Moneys.Domain.UseCases.Auth.LoginUser;

public class LoginUserHandler(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    IJwtIssuer jwtIssuer) : ILoginUserHandler
{
    public async Task<LoginInfo?> Handle(LoginUserCommand command)
    {
        var user = await userRepository.GetByEmail(command.Email);

        if (user is null)
            return null;
            
        var result = passwordHasher.VerifyPassword(command.Password, user.Password, user.PasswordSalt);

        if (!result)
            return null;

        var info = jwtIssuer.GenerateLoginInfo(user.Id);

        await refreshTokenRepository.Create(info.RefreshToken);

        return info;
    }
}
