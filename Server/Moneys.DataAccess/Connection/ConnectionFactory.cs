using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;

namespace Moneys.DataAccess.Connection;

public class ConnectionFactory : IConnectionFactory
{
    private readonly NpgsqlDataSource _source;

    public ConnectionFactory(string connectionString, ILoggerFactory loggerFactory)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

        dataSourceBuilder.UseLoggerFactory(loggerFactory);

        _source = dataSourceBuilder.Build();
    }

    public async Task<IDbConnection> OpenConnection(CancellationToken ct)
    {
        return await _source.OpenConnectionAsync(ct);
    }
}
