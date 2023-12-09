using Profile.Domain.Entities;

namespace Profile.Application.Commands;

public interface IProfileUpdatedCommand
{
    public Task ExecuteAsync(UserProfile userProfile, UserProfile updatedProfile);
}