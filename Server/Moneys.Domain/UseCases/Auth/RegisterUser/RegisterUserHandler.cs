using Moneys.Domain.Entities;
using Moneys.Domain.Repositories;

namespace Moneys.Domain.UseCases.Auth.RegisterUser;

public class RegisterUserHandler(IUserRepository userRepository) : IRegisterUserHandler
{
    public Task Register(User user)
    {
        return userRepository.Create(user);
    }
}
