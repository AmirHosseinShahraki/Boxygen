using Authentication.Application.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configs;

namespace Authentication.Infrastructure.Helpers.Injections;

internal static class MassTransitInjection
{
    public static IServiceCollection AddAuthMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        RabbitMQConfig rabbitmqConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>()!;

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserRegistrationConsumer>();
            x.AddConsumer<UserLoginConsumer>();
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