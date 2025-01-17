using System.Security.Claims;

namespace Moneys.Authentication.Services;

public interface IJwtIssuer
{
    string GenerateJwtToken(IEnumerable<Claim> claims, TokenType tokenType);

    DateTime GetDefaultValidityTime(TokenType tokenType);
}
