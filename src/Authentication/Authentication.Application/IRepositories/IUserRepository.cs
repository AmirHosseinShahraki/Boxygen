using Authentication.Domain.Entities;

namespace Authentication.Application.IRepositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User?> GetUserById(Guid userId);
    public Task<User?> GetUserByUsername(string username);
    public Task<User> CreateUser(User user);
    public Task<bool> UpdateUser(Guid userId, User updatedUser);
    public Task<bool> DeleteUser(Guid userId);
}