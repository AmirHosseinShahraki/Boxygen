namespace Email.Domain;

public class EmailVerification
{
    public EmailVerification(string emailAddress, string token, TimeSpan? expiryDuration = null)
    {
        EmailAddress = emailAddress;
        Token = token;
        CreatedAt = DateTime.Now;

        if (expiryDuration != null)
        {
            ExpiryDuration = expiryDuration.Value;
        }
    }

    public string EmailAddress { get; set; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan ExpiryDuration { get; set; } = TimeSpan.MaxValue;
}