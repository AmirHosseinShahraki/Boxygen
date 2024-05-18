using System.Security.Cryptography;
using Email.Application.Services.Interfaces;

namespace Email.Infrastructure.Services;

public class Base64UrlSafeTokenGenerator : ITokenGenerator
{
    public string Generate(int length = 32)
    {
        byte[] randomBytes = GenerateRandomBytes(length);
        string base64Token = Convert.ToBase64String(randomBytes);
        string token = Sanitize(base64Token);

        return token;
    }

    private static string Sanitize(string text)
    {
        return text.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    private static byte[] GenerateRandomBytes(int length)
    {
        byte[] randomBytes = new byte[length];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return randomBytes;
    }
}