using Authentication.Application.Services;
using Authentication.Domain.Repositories;
using Authentication.Infrastructure.Helpers;
using Authentication.Infrastructure.Helpers.Injections;
using Authentication.Infrastructure.Repositories;
using Authentication.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(
            configuration.GetSection("JwtConfiguration"));

        services.AddUserDatabase(configuration);

        services.AddMassTransit(configuration);

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IJwtService, JwtService>();

        return services;
    }
}