using Moneys.Domain.Entities;
using Moneys.Domain.Repositories;
using Moneys.Domain.Services.Contracts;

namespace Moneys.Domain.UseCases.Auth.RegisterUser;

public class RegisterUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRegisterUserHandler
{
    public Task Handle(User user, string password)
    {
        passwordHasher.HashPassword(password, out var hash, out var salt);

        user = user with
        {
            Password = hash,
            PasswordSalt = salt
        };

        return userRepository.Create(user);
    }
}
