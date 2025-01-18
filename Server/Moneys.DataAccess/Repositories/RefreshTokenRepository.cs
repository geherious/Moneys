using Dapper;
using Moneys.Domain.Entities;
using Moneys.Domain.Repositories;
using Npgsql;

namespace Moneys.DataAccess.Repositories;

public class RefreshTokenRepository(NpgsqlConnection connection) : IRefreshTokenRepository
{
    public async Task Create(RefreshToken token)
    {
        const string sql =
        """
        INSERT INTO refresh_tokens (user_id, hash, expires_at)
        VALUES (@UserId, @Hash, @ExpiresAt);
        """;

        await connection.ExecuteAsync(sql, token);
    }
}
