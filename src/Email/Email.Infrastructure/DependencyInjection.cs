using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddFluentEmail("")
            .AddHandlebarsRenderer()
            .AddSmtpSender("localhost", 25);

        return services;
    }
}