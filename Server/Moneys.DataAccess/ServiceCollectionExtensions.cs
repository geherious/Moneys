using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moneys.DataAccess.Connection;
using Moneys.DataAccess.Repositories;
using Moneys.Domain.Repositories;

namespace Moneys.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>(sp =>
            new ConnectionFactory(connectionString, sp.GetRequiredService<ILoggerFactory>()));
        
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
