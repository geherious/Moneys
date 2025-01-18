using Microsoft.Extensions.DependencyInjection;
using Moneys.Application.Services;
using Moneys.Domain.Services;
using Moneys.Domain.UseCases.Auth;
using Moneys.Domain.UseCases.Auth.LoginUser;
using Moneys.Domain.UseCases.Auth.RegisterUser;

namespace Moneys.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddUseCases();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IJwtIssuer, JwtIssuer>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
        services.AddScoped<ILoginUserHandler, LoginUserHandler>();

        return services;
    }
}
