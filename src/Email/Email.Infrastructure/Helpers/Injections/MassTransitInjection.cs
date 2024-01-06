using Email.Application.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configs;

namespace Email.Infrastructure.Helpers.Injections;

public static class MassTransitInjection
{
    public static IServiceCollection AddEmailMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitmqConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>()!;
        services.AddMassTransit(x =>
        {
            x.AddConsumer<SendVerificationEmailConsumer>();
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