using System.Security.Claims;
using Moneys.Domain.Entities;
using Moneys.Domain.Exceptions;
using Moneys.Domain.Repositories;
using Moneys.Domain.Services;

namespace Moneys.Domain.UseCases.Auth.LoginUser;

public class LoginUserHandler(
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHasher passwordHasher,
    IJwtIssuer jwtIssuer) : ILoginUserHandler
{
    public async Task<LoginInfo?> Handle(string email, string password)
    {
        var user = await userRepository.GetByEmail(email);

        if (user is null)
            return null;
            
        var result = passwordHasher.VerifyPassword(password, user.Password, user.PasswordSalt);

        if (!result)
            return null;

        var accessToken = GenerateJwtToken(user);

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Hash = Guid.NewGuid().ToString(),
            ExpiresAt = jwtIssuer.GetDefaultValidityTime(TokenType.RefreshToken)
        };

        await refreshTokenRepository.Create(refreshToken);

        return new LoginInfo
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private string GenerateJwtToken(User user)
    {
        return jwtIssuer.GenerateJwtToken(new[]
            {
                new Claim(Entities.ClaimTypes.UserId.ToString(), user.Id.ToString())
            },
            TokenType.AccessToken);
    }
}
