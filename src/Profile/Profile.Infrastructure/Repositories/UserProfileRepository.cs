using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Profile.Domain.Entities;
using Profile.Domain.Repositories;
using Profile.Infrastructure.Helpers;

namespace Profile.Infrastructure.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly IMongoCollection<UserProfile> _userProfileCollection;

    public UserProfileRepository(IMongoClient mongoClient,
        IOptions<UserProfileDatabaseConfig> userProfileDatabaseConfig)
    {
        IMongoDatabase database = mongoClient.GetDatabase(userProfileDatabaseConfig.Value.DatabaseName);
        _userProfileCollection = database.GetCollection<UserProfile>(userProfileDatabaseConfig.Value.CollectionName);
    }

    public async Task<UserProfile?> GetById(Guid id)
    {
        return await _userProfileCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<UserProfile> Create(UserProfile entity)
    {
        entity.Id = entity.Id;
        entity.CreatedAt = DateTime.Now;
        entity.UpdatedAt = entity.CreatedAt;
        await _userProfileCollection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<bool> Update(Guid id, UserProfile updatedEntity)
    {
        updatedEntity.Id = id;
        ReplaceOneResult result = await _userProfileCollection.ReplaceOneAsync(u => u.Id == id, updatedEntity);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        DeleteResult result = await _userProfileCollection.DeleteOneAsync(u => u.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}