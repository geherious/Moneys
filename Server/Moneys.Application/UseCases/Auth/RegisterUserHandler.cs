using Moneys.Domain.Entities;
using Moneys.Domain.Repositories;
using Moneys.Domain.Services;

namespace Moneys.Domain.UseCases.Auth.RegisterUser;

public class RegisterUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRegisterUserHandler
{
    public Task Handle(RegisterUserCommand command)
    {
        passwordHasher.HashPassword(command.Password, out var hash, out var salt);

        var user = new User
        {
            Email = command.Email,
            Password = hash,
            PasswordSalt = salt,
            RegisteredAt = DateTime.UtcNow
        };

        return userRepository.Create(user);
    }
}
