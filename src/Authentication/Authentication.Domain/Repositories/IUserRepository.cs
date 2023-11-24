using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User?> GetUserById(Guid userId);
    public Task<User?> GetUserByUsername(string username);
    public Task<bool> CheckUsernameExists(string username);
    public Task<User> CreateUser(User user);
    public Task<bool> UpdateUser(Guid userId, User updatedUser);
    public Task<bool> DeleteUser(Guid userId);
}