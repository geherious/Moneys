using Moneys.Domain.Entities;

namespace Moneys.Domain.UseCases.Auth;

public interface IRegisterUserHandler
{
    Task Handle(User user, string password);
}
