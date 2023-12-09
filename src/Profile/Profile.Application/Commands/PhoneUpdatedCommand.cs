using Profile.Domain.Entities;

namespace Profile.Application.Commands;

public class PhoneUpdatedCommand : IProfileUpdatedCommand
{
    public Task ExecuteAsync(UserProfile userProfile, UserProfile toBeUpdateProfile)
    {
        throw new NotImplementedException();
    }
}