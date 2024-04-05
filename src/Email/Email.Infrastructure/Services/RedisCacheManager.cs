using System.Text.Json;
using Email.Application.Services.Interfaces;
using StackExchange.Redis;

namespace Email.Infrastructure.Services;

public class RedisCacheManager : ICacheManager
{
    private readonly IDatabase _database;

    public RedisCacheManager(IDatabase database)
    {
        _database = database;
    }

    public async Task<T?> Get<T>(string key)
    {
        string? cachedValue = await _database.StringGetAsync(key);
        if (cachedValue is null)
        {
            return default;
        }

        T? deserializedObject = JsonSerializer.Deserialize<T>(cachedValue);
        return deserializedObject;
    }

    public async Task Add<T>(string key, T data, TimeSpan expiration)
    {
        string serializedJson = JsonSerializer.Serialize(data);
        await _database.StringSetAsync(key, serializedJson, expiration);
    }

    public async Task Remove(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}