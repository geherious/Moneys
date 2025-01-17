using System.Data;

namespace Moneys.DataAccess.Connection;

public interface IConnectionFactory
{
    Task<IDbConnection> OpenConnection(CancellationToken ct);
}
