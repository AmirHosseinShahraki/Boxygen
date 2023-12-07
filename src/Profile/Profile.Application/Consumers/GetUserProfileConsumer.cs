﻿using MassTransit;
using Profile.Domain.Messages;
using Profile.Domain.Queries;
using Profile.Domain.Repositories;

namespace Profile.Application.Consumers;

public class GetUserProfileConsumer : IConsumer<GetUserProfile>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public GetUserProfileConsumer(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task Consume(ConsumeContext<GetUserProfile> context)
    {
        var userProfileId = context.Message.Id;
        var userProfile = await _userProfileRepository.GetById(userProfileId);

        if (userProfile is null)
        {
            await context.RespondAsync(new UserProfileNotFound
            {
                Id = userProfileId
            });
            return;
        }

        await context.RespondAsync(userProfile);
    }
}