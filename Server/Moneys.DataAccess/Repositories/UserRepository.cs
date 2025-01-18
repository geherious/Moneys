using Dapper;
using Moneys.Domain.Entities;
using Moneys.Domain.Exceptions;
using Moneys.Domain.Repositories;
using Npgsql;

namespace Moneys.DataAccess.Repositories;

public class UserRepository(NpgsqlConnection connection) : IUserRepository
{
    public async Task Create(User user)
    {
        const string sql =
        """
            INSERT INTO users (email, password, password_salt, registered_at)
            VALUES (@Email, @Password, @PasswordSalt, @RegisteredAt)
            ON CONFLICT (email) DO NOTHING
            RETURNING id;
        """;

        var id = await connection.ExecuteScalarAsync<long?>(sql, user);

        if (id is null)
            throw new AlreadyExistsException(nameof(User.Email));
    }

    public async Task<User?> GetByEmail(string email)
    {
        const string sql = 
        """
        SELECT id as Id,
               email as Email,
               password as Password,
               password_salt as PasswordSalt,
               registered_at as RegisteredAt
        FROM users
        WHERE email = @Email;
        """;

        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });

        return user;
    }
}
