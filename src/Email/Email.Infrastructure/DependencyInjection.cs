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
        services.AddFluentEmail(smtpConfig.DefaultFrom, smtpConfig.DefaultName)
            .AddHandlebarsRenderer()
            .AddSmtpSender(smtpConfig.Host, smtpConfig.Port, smtpConfig.Username, smtpConfig.Password);

        services.AddEmailMassTransit(configuration);

        return services;
    }
}