using Moneys.Domain.Entities;

namespace Moneys.Domain.Repositories;

public interface IUserRepository
{
    Task Create(User user);
}
