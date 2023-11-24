using Authentication.Domain.Messages;

namespace Authentication.Application.Services;

public interface IJwtService
{
    public AuthToken GenerateToken(string username);
}