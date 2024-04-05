using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Email.Infrastructure.Helpers.Injections;

public static class RedisInjection
{
    public static IServiceCollection AddRedisDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetValue<string>("Redis:ConnectionString");
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
        IDatabase database = redis.GetDatabase();

        services.AddSingleton(database);

        return services;
    }
}