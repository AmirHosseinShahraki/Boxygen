using System.Security.Cryptography;
using System.Text;

namespace Email.Domain;

public class VerificationToken
{
    public VerificationToken(string emailAddress, string token, TimeSpan? expiryDuration = null)
    {
        HashedEmailAddress = Hash(emailAddress);
        Token = token;
        CreatedAt = DateTime.Now;

        if (expiryDuration != null)
        {
            ExpiryDuration = expiryDuration.Value;
        }
    }

    private static string Hash(string data)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        byte[] hashedBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashedBytes);
    }

    public string HashedEmailAddress { get; }
    public string Token { get; }
    public DateTime CreatedAt { get; }
    public TimeSpan ExpiryDuration { get; } = TimeSpan.MaxValue;
}