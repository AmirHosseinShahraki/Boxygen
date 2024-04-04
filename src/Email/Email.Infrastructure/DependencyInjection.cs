using Email.Application.Services;
using Email.Application.Services.Interfaces;
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
        services.Configure<SmtpConfig>(configuration.GetSection("SMTP"));
        services.Configure<TemplatesConfig>(configuration.GetSection("Templates"));

        services.AddTransient<ITokenGenerator, Base64UrlSafeTokenGenerator>();
        services.AddSingleton<IVerificationTokenManager, VerificationTokenManager>();

        services.AddSingleton<ITemplateProvider, HandlebarsTemplateProvider>();
        services.AddSingleton<IEmailService, SmtpEmailService>();
        services.AddSingleton<ICacheManager, RedisCacheManager>();

        services.AddEmailMassTransit(configuration);

        return services;
    }
}