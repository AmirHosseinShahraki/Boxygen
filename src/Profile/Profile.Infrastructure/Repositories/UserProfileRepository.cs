using Profile.Domain.Entities;
using Profile.Domain.Repositories;

namespace Profile.Infrastructure.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    public Task<UserProfile?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfile> Create(UserProfile entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Guid id, UserProfile updatedEntity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}