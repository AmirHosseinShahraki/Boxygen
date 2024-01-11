using Email.Infrastructure.Helpers;
using Email.Infrastructure.Helpers.Injections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var smtpConfig = configuration.GetSection("SMTP").Get<SMTPConfig>()!;

        services.AddEmailMassTransit(configuration);

        return services;
    }
}