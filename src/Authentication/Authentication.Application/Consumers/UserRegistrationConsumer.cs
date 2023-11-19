using Authentication.Application.IRepositories;
using Authentication.Domain.Commands;
using Authentication.Domain.Entities;
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
        var user = new User()
        {
            Username = context.Message.Username,
            Password = context.Message.Password
        };
        var createdUser = await _userRepository.CreateUser(user);

        var newUserRegisteredEvent = new NewUserRegistered()
        {
            Id = createdUser.Id.ToString(),
            Username = context.Message.Username,
        };
        await context.RespondAsync(newUserRegisteredEvent);
    }
}