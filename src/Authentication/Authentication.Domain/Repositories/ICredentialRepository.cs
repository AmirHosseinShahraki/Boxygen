using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories;

public interface ICredentialRepository
{
    public Task<IEnumerable<Credential>> GetAllCredentials();
    public Task<Credential?> GetCredentialsById(Guid userId);
    public Task<Credential?> GetCredentialsByUsername(string username);
    public Task<bool> CheckUsernameExists(string username);
    public Task<Credential> CreateCredentials(Credential credential);
    public Task<bool> UpdateCredentials(Guid credentialId, Credential updatedCredential);
    public Task<bool> DeleteCredentials(Guid credentialId);
}