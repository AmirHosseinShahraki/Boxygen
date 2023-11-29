using Authentication.Domain.Entities;
using Shared;

namespace Authentication.Domain.Repositories;

public interface ICredentialRepository : IBaseRepository<Credential>
{
    public Task<Credential?> GetByUsername(string username);
    public Task<bool> CheckUsernameExists(string username);
}