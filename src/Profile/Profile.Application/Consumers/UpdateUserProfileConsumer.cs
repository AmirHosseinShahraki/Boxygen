using AutoMapper;
using MassTransit;
using Profile.Application.Commands;
using Profile.Domain.Commands;
using Profile.Domain.Entities;
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
        UserProfile toBeUpdateProfile = context.Message.Profile;
        Guid id = context.Message.UserProfileId;
        UserProfile? userProfile = await _userProfileRepository.GetById(id);
        if (userProfile is null)
        {
            await context.RespondAsync(new UserProfileNotFound { Id = id });
            return;
        }

        UserProfile updatedProfile = _mapper.Map(toBeUpdateProfile, userProfile);
        updatedProfile.UpdatedAt = DateTime.Now;
        await _userProfileRepository.Update(id, updatedProfile);

        await context.RespondAsync(updatedProfile);

        await _updatedCommandInvoker.ExecuteCommands(userProfile, updatedProfile);
    }
}