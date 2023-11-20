using Authentication.Application.IRepositories;
using Authentication.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Authentication.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;

    public UserRepository(IMongoClient mongoClient, IOptions<UserDatabaseSettings> userDatabaseSettings)
    {
        var database = mongoClient.GetDatabase(userDatabaseSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<User>(userDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var users = await _userCollection.Find(new BsonDocument()).ToListAsync();
        return users;
    }

    public async Task<User> GetUserById(Guid userId)
    {
        var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();
        return user;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        var user = await _userCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        return user;
    }

    public async Task<User> CreateUser(User user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = user.CreatedAt;
        await _userCollection.InsertOneAsync(user);
        return user;
    }

    public async Task<bool> UpdateUser(Guid userId, User updatedUser)
    {
        var result = await _userCollection.ReplaceOneAsync(u => u.Id == userId, updatedUser);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteUser(Guid userId)
    {
        var result = await _userCollection.DeleteOneAsync(u => u.Id == userId);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}