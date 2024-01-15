namespace Email.Application.Services.Interfaces;

public interface ICacheManager
{
    Task<T> Get<T>(string key);
    Task Add<T>(string key, T data, TimeSpan expiration);
    Task Remove(string key);
}