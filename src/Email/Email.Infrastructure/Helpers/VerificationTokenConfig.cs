namespace Email.Infrastructure.Helpers;

public class VerificationTokenConfig
{
    public int TokenLength { get; init; }
    public TimeSpan TokenExpiration { get; init; }
    public string VerificationBaseUrl { get; init; } = null!;
}