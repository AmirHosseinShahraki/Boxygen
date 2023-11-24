namespace Authentication.Infrastructure.Helpers;

public class JwtConfiguration
{
    public string PrivateKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public TimeSpan ExpiryTimeSpan { get; set; }
}