using Hangfire;
using Hangfire.MemoryStorage;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddHangfire(h =>
        {
            h.UseMemoryStorage();
            h.UseRecommendedSerializerSettings();
        });
        services.AddHangfireServer();

        services.AddMassTransit(x =>
        {
            UserRegistrationDatabaseConfig userRegistrationDbConfig = configuration.GetSection("UserRegistrationDatabase").Get<UserRegistrationDatabaseConfig>()!;
            RabbitMQConfig rabbitmqConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>()!;

            x.AddSagaStateMachine<UserRegistrationStateMachine, UserRegistrationState>()
                .MongoDbRepository(r =>
                {
                    r.Connection = userRegistrationDbConfig.ConnectionString;
                    r.DatabaseName = userRegistrationDbConfig.DatabaseName;
                    r.CollectionName = userRegistrationDbConfig.CollectionName;
                });
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseInMemoryOutbox(context);
                cfg.Host(rabbitmqConfig.Host, h =>
                {
                    h.Username(rabbitmqConfig.Username);
                    h.Password(rabbitmqConfig.Password);
                });
                cfg.UseHangfireScheduler();
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}