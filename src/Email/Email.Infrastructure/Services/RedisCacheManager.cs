using Email.Application.Services.Interfaces;

namespace Email.Infrastructure.Services;

public class RedisCacheManager : ICacheManager
{
    public Task<T> Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task Add<T>(string key, T data, TimeSpan expiration)
    {
        throw new NotImplementedException();
    }

    public Task Remove(string key)
    {
        throw new NotImplementedException();
    }
}