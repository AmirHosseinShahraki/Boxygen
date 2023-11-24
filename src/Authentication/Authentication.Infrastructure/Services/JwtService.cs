using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Authentication.Application.Services;
using Authentication.Domain.Messages;
using Authentication.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtConfiguration _configuration;
    private readonly SigningCredentials _credentials;
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;

    public JwtService(IOptions<JwtConfiguration> configuration)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        _configuration = configuration.Value;
        var rsa = RSA.Create();
        rsa.ImportFromPem(_configuration.PrivateKey.ToCharArray());
        var securityKey = new RsaSecurityKey(rsa);
        _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512);
        _jwtTokenHandler = new JwtSecurityTokenHandler();
    }

    public AuthToken GenerateToken(string username)
    {
        var claims = new[]
        {
            new Claim("sub", username),
        };
        var expires = DateTime.UtcNow.Add(_configuration.ExpiryTimeSpan);
        var token = new JwtSecurityToken(_configuration.Issuer,
            _configuration.Audience,
            claims,
            expires: expires,
            signingCredentials: _credentials);

        return new AuthToken()
        {
            AccessToken = _jwtTokenHandler.WriteToken(token),
            ExpiresAt = expires
        };
    }
}