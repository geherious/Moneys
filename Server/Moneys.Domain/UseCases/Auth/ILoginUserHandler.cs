using Moneys.Domain.Entities;

namespace Moneys.Domain.UseCases.Auth;

public interface ILoginUserHandler
{
    Task<LoginInfo?> Handle(string email, string password);
}
