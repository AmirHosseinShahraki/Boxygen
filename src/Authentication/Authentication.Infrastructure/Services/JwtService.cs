﻿using System.IdentityModel.Tokens.Jwt;
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
        RSA rsa = RSA.Create();
        rsa.ImportFromPem(_configuration.PrivateKey.ToCharArray());
        RsaSecurityKey securityKey = new(rsa);
        _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512);
        _jwtTokenHandler = new JwtSecurityTokenHandler();
    }

    public AuthToken GenerateToken(Guid id, string username)
    {
        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, username)
        };
        DateTime expires = DateTime.Now.Add(_configuration.ExpiryTimeSpan);
        JwtSecurityToken token = new(_configuration.Issuer,
            _configuration.Audience,
            claims,
            expires: expires,
            signingCredentials: _credentials);

        return new AuthToken
        {
            AccessToken = _jwtTokenHandler.WriteToken(token),
            ExpiresAt = expires
        };
    }
}