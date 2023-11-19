using Authentication.Application.Consumers;
using Authentication.Application.IRepositories;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Repositories;
using MassTransit;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


namespace Authentication.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<UserDatabaseSettings>(options =>
            configuration.GetSection("UserDatabaseSettings").Bind(options));
        var userDatabaseSettings = configuration.GetSection("UserDatabaseSettings").Get<UserDatabaseSettings>()!;
        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = userDatabaseSettings!.ConnectionString;
            return new MongoClient(connectionString);
        });
        services.AddSingleton<IUserRepository, UserRepository>();

        BsonClassMap.RegisterClassMap<User>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
        });

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserRegistrationConsumer>();
            x.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });


        return services;
    }
}