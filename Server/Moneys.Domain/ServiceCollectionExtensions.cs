using Microsoft.Extensions.DependencyInjection;
using Moneys.Domain.UseCases.Auth.RegisterUser;

namespace Moneys.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>(); 

        return services;
    }
}
