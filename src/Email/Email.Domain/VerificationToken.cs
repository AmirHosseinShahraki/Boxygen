using System.Security.Cryptography;
using System.Text;

namespace Email.Domain;

public class VerificationToken
{
    private readonly string _hashedEmailAddress = null!;
    public Guid Id { get; init; }

    public string HashedEmailAddress
    {
        get => _hashedEmailAddress;
        init => _hashedEmailAddress = Hash(value);
    }

    public string Token { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public TimeSpan ExpiryDuration { get; init; } = TimeSpan.MaxValue;
    public string VerificationBaseUrl { get; init; } = null!;
    public Uri ResponseAddress { get; init; } = null!;

    public bool IsHashedEmailEqual(string email)
    {
        return HashedEmailAddress == Hash(email);
    }

    public bool IsExpired()
    {
        return CreatedAt + ExpiryDuration < DateTime.Now;
    }


    private static string Hash(string data)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(data);
        byte[] hashedBytes = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hashedBytes);
    }
}