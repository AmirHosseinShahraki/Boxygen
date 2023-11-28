namespace Authentication.Infrastructure.Helpers;

public class CredentialDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CredentialsCollectionName { get; set; } = null!;
}