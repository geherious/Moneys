using Moneys.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Moneys.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptionsSection = configuration.GetRequiredSection("Jwt");
        services.Configure<JwtOptions>(jwtOptionsSection);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOptions =>
        {
            var configKey = jwtOptionsSection["Key"]!;
            var key = Encoding.UTF8.GetBytes(configKey);

            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtOptionsSection["Issuer"],
                ValidAudience = jwtOptionsSection["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}
