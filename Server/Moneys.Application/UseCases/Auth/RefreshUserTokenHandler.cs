using Moneys.Domain.Entities;
using Moneys.Domain.Repositories;
using Moneys.Domain.Services;
using Moneys.Domain.UseCases.Auth;

namespace Moneys.Application.UseCases.Auth;

public class RefreshUserTokenHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IJwtIssuer jwtIssuer
) : IRefreshUserTokenHandler
{
    public async Task<LoginInfo?> Handle(RefreshUserTokenCommand command)
    {
        var token = await refreshTokenRepository.GetAndDelete(command.Hash);

        if (token is null)
            return null;
        
        if (token.ExpiresAt < DateTime.UtcNow)
        {
            return null;
        }

        var info = jwtIssuer.GenerateLoginInfo(token.UserId);

        await refreshTokenRepository.Create(info.RefreshToken);

        return info;
    }
}
