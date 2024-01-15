using System.Security.Cryptography;
using Email.Application.Services.Interfaces;

namespace Email.Application.Services;

public class Base64UrlSafeTokenGenerator : ITokenGenerator
{
    public string Generate(int length = 32)
    {
        byte[] randomBytes = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        string base64Token = Convert.ToBase64String(randomBytes);
        // Make the Base64 string URL-safe by replacing specific characters
        string token = base64Token.Replace('+', '-').Replace('/', '_').TrimEnd('=');

        return token;
    }
}