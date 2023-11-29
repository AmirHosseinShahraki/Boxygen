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
    private readonly IBus _bus;
    private readonly ICredentialRepository _credentialRepository;

    public UserRegistrationConsumer(IBus bus, ICredentialRepository credentialRepository)
    {
        _bus = bus;
        _credentialRepository = credentialRepository;
    }

    public async Task Consume(ConsumeContext<RegisterUser> context)
    {
        var usernameExists = await _credentialRepository.CheckUsernameExists(context.Message.Username);
        if (usernameExists)
        {
            var usernameTakenEvent = new UsernameTaken();
            await context.RespondAsync(usernameTakenEvent);
            return;
        }

        var hashedPassword = BC.EnhancedHashPassword(context.Message.Password, HashType.SHA512);
        var createdUser = await _credentialRepository.Create(new Credential()
        {
            Username = context.Message.Username,
            Password = hashedPassword
        });

        var newUserRegisteredEvent = new NewUserRegistered()
        {
            Id = createdUser.Id,
            Username = context.Message.Username,
            RegisteredAt = DateTime.UtcNow
        };
        await context.RespondAsync(newUserRegisteredEvent);
        await _bus.Publish(newUserRegisteredEvent);
    }
}