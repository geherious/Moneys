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

    public async Task<RefreshToken?> GetAndDelete(string hash)
    {
        const string sql =
        """
        WITH deleted AS (
            DELETE FROM refresh_tokens
                  where hash = @Hash
              returning *
        )
        select user_id as UserId,
                  hash as Hash,
            expires_at as ExpiresAt
        from deleted;
        """;

        var parameters = new
        {
            Hash = hash
        };

        var token = await connection.QueryFirstOrDefaultAsync<RefreshToken>(sql, parameters);

        return token;
    }
}
