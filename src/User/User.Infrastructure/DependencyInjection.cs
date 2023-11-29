using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Shared.Configs;
using User.Application.StateMachines;
using User.Domain.States;
using User.Infrastructure.Helpers;

namespace User.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        BsonClassMap.RegisterClassMap<UserRegistrationState>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.CorrelationId)
                .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
        });

        services.AddMassTransit(x =>
        {
            var userRegistrationDbConfig = configuration.GetSection("UserRegistrationDatabase").Get<UserRegistrationDatabaseConfig>()!;
            var rabbitmqConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>()!;

            var schedulerEndpoint = new Uri("queue:scheduler");
            x.AddMessageScheduler(schedulerEndpoint);
            x.AddSagaStateMachine<UserRegistrationStateMachine, UserRegistrationState>().MongoDbRepository(r =>
            {
                r.Connection = userRegistrationDbConfig.ConnectionString;
                r.DatabaseName = userRegistrationDbConfig.DatabaseName;
                r.CollectionName = userRegistrationDbConfig.CollectionName;
            });
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseMessageScheduler(schedulerEndpoint);
                cfg.Host(rabbitmqConfig.Host, h =>
                {
                    h.Username(rabbitmqConfig.Username);
                    h.Password(rabbitmqConfig.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}