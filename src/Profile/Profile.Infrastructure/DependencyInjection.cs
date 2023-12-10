using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profile.Application.Utilities;
using Profile.Domain.Repositories;
using Profile.Infrastructure.Helpers.Injections;
using Profile.Infrastructure.Repositories;

namespace Profile.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddUserProfileDatabase(configuration);
        services.AddProfileMassTransit(configuration);
        services.AddSingleton<IUserProfileRepository, UserProfileRepository>();
        services.AddAutoMapper(typeof(ApplicationAutoMapperProfile));
        return services;
    }
}