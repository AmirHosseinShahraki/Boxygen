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
        UserProfileDatabaseConfig userProfileDatabaseConfig = configuration.GetSection("UserProfileDatabase").Get<UserProfileDatabaseConfig>()!;

        services.AddSingleton<IMongoClient>(sp =>
        {
            string connectionString = userProfileDatabaseConfig.ConnectionString;
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