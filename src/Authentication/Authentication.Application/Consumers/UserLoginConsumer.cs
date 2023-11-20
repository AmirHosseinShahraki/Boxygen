﻿using Authentication.Application.IRepositories;
using Authentication.Domain.Commands;
using Authentication.Domain.Messages;
using BCrypt.Net;
using MassTransit;

namespace Authentication.Application.Consumers;

public class UserLoginConsumer : IConsumer<LoginUser>
{
    private readonly IUserRepository _userRepository;

    public UserLoginConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<LoginUser> context)
    {
        var user = await _userRepository.GetUserByUsername(context.Message.Username);

        if (user is null || !BC.EnhancedVerify(context.Message.Password, user.Password, HashType.SHA512))
        {
            await context.RespondAsync(new AuthFailed()
            {
                Username = context.Message.Username
            });
            return;
        }

        await context.RespondAsync(new AuthToken()
        {
            AccessToken = "AccessToken",
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        });
    }
}