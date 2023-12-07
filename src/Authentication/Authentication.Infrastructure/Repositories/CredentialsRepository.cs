using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Authentication.Infrastructure.Repositories;

public class CredentialsRepository : ICredentialRepository
{
    private readonly IMongoCollection<Credential> _credentialCollection;

    public CredentialsRepository(IMongoClient mongoClient, IOptions<CredentialDatabaseSettings> credentialDatabaseSettings)
    {
        var database = mongoClient.GetDatabase(credentialDatabaseSettings.Value.DatabaseName);
        _credentialCollection = database.GetCollection<Credential>(credentialDatabaseSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<Credential>> GetAllCredentials()
    {
        return await _credentialCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Credential?> GetById(Guid userId)
    {
        return await _credentialCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<Credential?> GetByUsername(string username)
    {
        return await _credentialCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
    }

    public Task<bool> CheckUsernameExists(string username)
    {
        return _credentialCollection.Find(u => u.Username == username).AnyAsync();
    }

    public async Task<Credential> Create(Credential credential)
    {
        credential.Id = Guid.NewGuid();
        credential.CreatedAt = DateTime.Now;
        credential.UpdatedAt = credential.CreatedAt;
        await _credentialCollection.InsertOneAsync(credential);
        return credential;
    }

    public async Task<bool> Update(Guid credentialId, Credential updatedCredential)
    {
        var result = await _credentialCollection.ReplaceOneAsync(u => u.Id == credentialId, updatedCredential);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(Guid credentialId)
    {
        var result = await _credentialCollection.DeleteOneAsync(u => u.Id == credentialId);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}