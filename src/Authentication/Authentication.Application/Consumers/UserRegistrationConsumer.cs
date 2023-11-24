using Authentication.Domain.Commands;
using Authentication.Domain.Entities;
using Authentication.Domain.Messages;
using Authentication.Domain.Repositories;
using BCrypt.Net;
using MassTransit;
using Shared.Events;

namespace Authentication.Application.Consumers;

public class UserRegistrationConsumer : IConsumer<RegisterUser>
{
    private readonly IUserRepository _userRepository;

    public UserRegistrationConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<RegisterUser> context)
    {
        var usernameExists = await _userRepository.CheckUsernameExists(context.Message.Username);
        if (usernameExists)
        {
            var usernameTakenEvent = new UsernameTaken();
            await context.RespondAsync(usernameTakenEvent);
            return;
        }

        var hashedPassword = BC.EnhancedHashPassword(context.Message.Password, HashType.SHA512);
        var createdUser = await _userRepository.CreateUser(new User()
        {
            Username = context.Message.Username,
            Password = hashedPassword
        });

        var newUserRegisteredEvent = new NewUserRegistered()
        {
            Id = createdUser.Id.ToString(),
            Username = context.Message.Username,
        };
        await context.RespondAsync(newUserRegisteredEvent);
    }
}