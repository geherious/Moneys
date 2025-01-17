using Moneys.Domain.Entities;

namespace Moneys.Domain.UseCases.Auth.RegisterUser;

public interface IRegisterUserHandler
{
    Task Register(User user);
}
