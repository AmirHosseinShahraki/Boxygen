﻿using Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Authentication.Infrastructure.Helpers.Injections;

internal static class CredentialDatabaseInjection
{
    public static IServiceCollection AddCredentialDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CredentialDatabaseSettings>(options =>
            configuration.GetSection("CredentialDatabaseSettings").Bind(options));
        CredentialDatabaseSettings credentialDatabaseSettings = configuration.GetSection("CredentialDatabaseSettings").Get<CredentialDatabaseSettings>()!;

        services.AddSingleton<IMongoClient>(sp =>
        {
            string connectionString = credentialDatabaseSettings.ConnectionString;
            return new MongoClient(connectionString);
        });

        BsonClassMap.RegisterClassMap<Credential>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id);
        });

        return services;
    }
}