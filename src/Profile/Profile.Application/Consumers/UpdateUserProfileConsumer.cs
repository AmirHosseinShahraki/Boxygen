﻿using AutoMapper;
using MassTransit;
using Profile.Application.Commands;
using Profile.Domain.Commands;
using Profile.Domain.Messages;
using Profile.Domain.Repositories;

namespace Profile.Application.Consumers;

public class UpdateUserProfileConsumer : IConsumer<UpdateUserProfile>
{
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly ProfileUpdatedCommandInvoker _updatedCommandInvoker;

    public UpdateUserProfileConsumer(IMapper mapper, IUserProfileRepository userProfileRepository)
    {
        _mapper = mapper;
        _userProfileRepository = userProfileRepository;
        _updatedCommandInvoker = new ProfileUpdatedCommandInvoker();
    }

    public async Task Consume(ConsumeContext<UpdateUserProfile> context)
    {
        var toBeUpdateProfile = context.Message.Profile;
        var id = context.Message.UserProfileId;
        var userProfile = await _userProfileRepository.GetById(id);
        if (userProfile is null)
        {
            await context.RespondAsync(new UserProfileNotFound { Id = id });
            return;
        }

        var updatedProfile = _mapper.Map(toBeUpdateProfile, userProfile);
        updatedProfile.UpdatedAt = DateTime.Now;
        await _userProfileRepository.Update(id, updatedProfile);

        await context.RespondAsync(updatedProfile);

        await _updatedCommandInvoker.ExecuteCommands(userProfile, updatedProfile);
    }
}