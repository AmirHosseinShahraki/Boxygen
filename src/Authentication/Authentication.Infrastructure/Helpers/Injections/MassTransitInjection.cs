using Authentication.Application.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure.Helpers.Injections;

internal static class MassTransitInjection
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserRegistrationConsumer>();
            x.AddConsumer<UserLoginConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}