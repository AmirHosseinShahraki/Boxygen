using Email.Application.Services;
using Email.Infrastructure.Helpers;
using Email.Infrastructure.Helpers.Injections;
using Email.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Email.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SMTPConfig>(configuration.GetSection("SMTP"));
        services.Configure<TemplatesConfig>(configuration.GetSection("Templates"));

        services.AddSingleton<ITemplateProvider, TemplateProvider>();
        services.AddSingleton<IEmailService, EmailService>();

        services.AddEmailMassTransit(configuration);

        return services;
    }
}