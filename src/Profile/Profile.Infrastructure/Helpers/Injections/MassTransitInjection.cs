using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profile.Application.Consumers;
using Shared.Configs;

namespace Profile.Infrastructure.Helpers.Injections;

public static class MassTransitInjection
{
    public static IServiceCollection AddProfileMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        RabbitMQConfig rabbitmqConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>()!;
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateUserProfileConsumer>();
            x.AddConsumer<SubmitUserProfileConsumer>();
            x.AddConsumer<GetUserProfileConsumer>();
            x.AddConsumer<UpdateUserProfileConsumer>();
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
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