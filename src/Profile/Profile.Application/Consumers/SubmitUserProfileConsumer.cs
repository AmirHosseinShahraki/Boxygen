using AutoMapper;
using MassTransit;
using Profile.Domain.Commands;
using Profile.Domain.Entities;
using Profile.Domain.Messages;
using Profile.Domain.Repositories;
using Shared.Events;

namespace Profile.Application.Consumers;

public class SubmitUserProfileConsumer : IConsumer<SubmitUserProfile>
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;

    public SubmitUserProfileConsumer(IBus bus, IMapper mapper, IUserProfileRepository userProfileRepository)
    {
        _bus = bus;
        _mapper = mapper;
        _userProfileRepository = userProfileRepository;
    }

    public async Task Consume(ConsumeContext<SubmitUserProfile> context)
    {
        UserProfile toBeSubmitProfile = context.Message.Profile;
        Guid id = context.Message.UserProfileId;

        UserProfile? userProfile = await _userProfileRepository.GetById(id);
        if (userProfile is null)
        {
            await context.RespondAsync(new UserProfileNotFound { Id = id });
            return;
        }

        UserProfile submittedProfile = _mapper.Map(toBeSubmitProfile, userProfile);
        await _userProfileRepository.Update(id, submittedProfile);

        await context.RespondAsync(submittedProfile);

        ProfileSubmitted profileSubmittedEvent = _mapper.Map<ProfileSubmitted>(submittedProfile);
        await _bus.Publish(profileSubmittedEvent);
    }
}