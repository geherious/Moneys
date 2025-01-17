using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Moneys.Authentication.Services;

public class JwtIssuer(IConfiguration _configuration) : IJwtIssuer
{
    public string GenerateJwtToken(IEnumerable<Claim> claims, TokenType tokenType)
    {
            DateTime expires = GetDefaultValidityTime(tokenType);

            var issuer = _configuration.GetRequiredSection("JWT:Issuer").Value;
            var audience = _configuration.GetRequiredSection("JWT:Audience").Value;
            var keyString = _configuration.GetRequiredSection("JWT:Key").Value;

            if (issuer == null || audience == null || keyString == null)
                throw new ArgumentException("JwtIssuer: missing configuration");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            claims = claims.Append(new Claim("Seed", Guid.NewGuid().ToString()));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
    }

    public DateTime GetDefaultValidityTime(TokenType tokenType)
    {
        switch(tokenType)
        {
            case TokenType.AccessToken:
                var minutesConf = _configuration.GetRequiredSection("JWT:AccessTokenMinutes").Value
                    ?? throw new ArgumentException("JwtIssuer: missing configuration: JWT:AccessTokenMinutes");

                int minutes = int.Parse(minutesConf);

                return DateTime.UtcNow.AddMinutes(minutes);

            case TokenType.RefreshToken:
                var daysConf = _configuration.GetRequiredSection("JWT:RefreshTokenDays").Value
                    ?? throw new ArgumentException("JwtIssuer: missing configuration: JWT:RefreshTokenDays");

                int days = int.Parse(daysConf);

                return DateTime.UtcNow.AddDays(days);
            default:
                throw new ArgumentException($"{tokenType} is not supported");
        }
    }
}
