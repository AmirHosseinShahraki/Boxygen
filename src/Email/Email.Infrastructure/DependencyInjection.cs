using Email.Application.Services.Interfaces;
using Email.Infrastructure.Helpers;
using Email.Infrastructure.Helpers.Injections;
using Email.Infrastructure.Services;
using Email.Infrastructure.Services.TokenValidator;
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
        services.Configure<VerificationTokenConfig>(configuration.GetSection("VerificationToken"));

        services.AddTransient<ITokenGenerator, Base64UrlSafeTokenGenerator>();
        services.AddSingleton<IVerificationTokenGenerator, VerificationTokenGenerator>();
        services.AddSingleton<IVerificationTokenValidator, VerificationTokenValidator>();

        services.AddSingleton<ITemplateProvider, HandlebarsTemplateProvider>();
        services.AddSingleton<IEmailService, SmtpEmailService>();
        services.AddSingleton<ICacheManager, RedisCacheManager>();

        services.AddEmailMassTransit(configuration);
        services.AddRedisDatabase(configuration);

        return services;
    }
}