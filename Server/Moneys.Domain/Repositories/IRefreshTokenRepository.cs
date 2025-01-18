using Moneys.Domain.Entities;

namespace Moneys.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task Create(RefreshToken token);
}
