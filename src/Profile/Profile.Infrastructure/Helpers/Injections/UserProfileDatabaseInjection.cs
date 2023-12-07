using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Profile.Domain.Entities;

namespace Profile.Infrastructure.Helpers.Injections;

public static class UserProfileDatabaseInjection
{
    public static IServiceCollection AddUserProfileDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<UserProfileDatabaseConfig>(options =>
            configuration.GetSection("UserProfileDatabase").Bind(options));
        var userProfileDatabaseConfig = configuration.GetSection("UserProfileDatabase").Get<UserProfileDatabaseConfig>()!;

        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = userProfileDatabaseConfig.ConnectionString;
            return new MongoClient(connectionString);
        });

        BsonClassMap.RegisterClassMap<UserProfile>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id);
        });

        return services;
    }
}