using Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Authentication.Infrastructure.Helpers.Injections;

internal static class UserDatabaseInjection
{
    public static IServiceCollection AddUserDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<UserDatabaseSettings>(options =>
            configuration.GetSection("UserDatabaseSettings").Bind(options));
        var userDatabaseSettings = configuration.GetSection("UserDatabaseSettings").Get<UserDatabaseSettings>()!;
        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = userDatabaseSettings.ConnectionString;
            return new MongoClient(connectionString);
        });
        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
        });
        return services;
    }
}