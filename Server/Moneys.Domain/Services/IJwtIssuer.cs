using System.Security.Claims;
using Moneys.Domain.Entities;

namespace Moneys.Domain.Services;

public interface IJwtIssuer
{
    string GenerateJwtToken(IEnumerable<Claim> claims, TokenType tokenType);

    DateTime GetDefaultValidityTime(TokenType tokenType);
}
