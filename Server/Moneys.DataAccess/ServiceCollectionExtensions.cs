using Microsoft.Extensions.DependencyInjection;
using Moneys.DataAccess.Repositories;
using Moneys.Domain.Repositories;

namespace Moneys.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection services, string connectionString)
    {
        services.AddNpgsqlDataSource(connectionString);
        
        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}
