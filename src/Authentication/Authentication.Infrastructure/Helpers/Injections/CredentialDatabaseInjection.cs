using Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Authentication.Infrastructure.Helpers.Injections;

internal static class CredentialDatabaseInjection
{
    public static IServiceCollection AddCredentialDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CredentialDatabaseSettings>(options =>
            configuration.GetSection("CredentialDatabaseSettings").Bind(options));
        var credentialDatabaseSettings = configuration.GetSection("CredentialDatabaseSettings").Get<CredentialDatabaseSettings>()!;
        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = credentialDatabaseSettings.ConnectionString;
            return new MongoClient(connectionString);
        });
        BsonClassMap.RegisterClassMap<Credential>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
        });
        return services;
    }
}