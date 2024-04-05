using System.Security.Cryptography;
using System.Text;

namespace Email.Domain;

public class VerificationToken
{
    public VerificationToken(string emailAddress, string token, string verificationBaseUrl,
        TimeSpan? expiryDuration = null)
    {
        HashedEmailAddress = Hash(emailAddress);
        Token = token;
        VerificationBaseUrl = verificationBaseUrl;
        CreatedAt = DateTime.Now;

        if (expiryDuration != null)
        {
            ExpiryDuration = expiryDuration.Value;
        }
    }

    public string HashedEmailAddress { get; }
    public string Token { get; }
    public DateTime CreatedAt { get; }
    public TimeSpan ExpiryDuration { get; } = TimeSpan.MaxValue;
    public string VerificationBaseUrl { get; }

    private static string Hash(string data)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        byte[] hashedBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashedBytes);
    }
}