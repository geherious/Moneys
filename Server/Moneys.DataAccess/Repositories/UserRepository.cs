using Dapper;
using Moneys.DataAccess.Connection;
using Moneys.Domain.Entities;
using Moneys.Domain.Exceptions;
using Moneys.Domain.Repositories;

namespace Moneys.DataAccess.Repositories;

public class UserRepository(IConnectionFactory connectionFactory) : IUserRepository
{
    public async Task Create(User user)
    {
        const string sql =
        $"""
        WITH ins AS (
            INSERT INTO users (username, email, password, password_salt, registered_at)
            VALUES (@Username, @Email, @Password, @PasswordSalt, @RegisteredAt)
            ON CONFLICT (username) DO NOTHING
            RETURNING username
        )
        SELECT 
            CASE 
                WHEN (SELECT COUNT(*) FROM ins) > 0 THEN 'inserted'
                WHEN EXISTS (SELECT 1 FROM users WHERE username = @Username) THEN 'username'
                WHEN EXISTS (SELECT 1 FROM users WHERE email = @Email) THEN 'email'
            END AS message;
        """;

        using var connection = await connectionFactory.OpenConnection(CancellationToken.None);

        var message = await connection.ExecuteScalarAsync<string>(sql, user);

        if (message == "inserted")
            return;
        else if (message == "username")
            throw new AlreadyExistsException(nameof(User.Username));
        else if (message == "email")
            throw new AlreadyExistsException(nameof(User.Email));
    }
}
