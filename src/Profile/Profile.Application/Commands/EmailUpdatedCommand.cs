using Profile.Domain.Entities;

namespace Profile.Application.Commands;

public class EmailUpdatedCommand : IProfileUpdatedCommand
{
    public Task ExecuteAsync(UserProfile userProfile, UserProfile toBeUpdateProfile)
    {
        throw new NotImplementedException();
    }
}