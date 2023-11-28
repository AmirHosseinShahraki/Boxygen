namespace User.Infrastructure.Helpers;

public record UserRegistrationDatabaseConfig
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UserRegistrationsCollectionName { get; set; } = null!;
}