using Microsoft.Extensions.DependencyInjection;
using Moneys.Domain.Services.Contracts;
using Moneys.Domain.Services.Implementations;
using Moneys.Domain.UseCases.Auth.RegisterUser;

namespace Moneys.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
