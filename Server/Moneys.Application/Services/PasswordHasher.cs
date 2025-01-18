using System.Security.Cryptography;
using System.Text;
using Moneys.Domain.Services;

namespace Moneys.Application.Services;

public class PasswordHasher : IPasswordHasher
{
    public void HashPassword(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA256();

        salt = hmac.Key;

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        hash = hmac.ComputeHash(passwordBytes);
    }

    public bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA256(salt);

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        byte[] computedHash = hmac.ComputeHash(passwordBytes);

        bool hashesMatch = computedHash.SequenceEqual(hash);

        return hashesMatch;
    }
}
