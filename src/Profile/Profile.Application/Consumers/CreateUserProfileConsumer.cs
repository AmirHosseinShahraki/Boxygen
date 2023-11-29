using MassTransit;
using Profile.Domain.Repositories;
using Shared.Commands;

namespace Profile.Application.Consumers;

public class CreateUserProfileConsumer : IConsumer<CreateProfile>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public CreateUserProfileConsumer(IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public Task Consume(ConsumeContext<CreateProfile> context)
    {
        throw new NotImplementedException();
    }
}