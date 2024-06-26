﻿using Email.Application.Services.Interfaces;
using Email.Domain;
using Email.Infrastructure.Helpers;
using Microsoft.Extensions.Options;

namespace Email.Infrastructure.Services;

public class VerificationTokenGenerator : IVerificationTokenGenerator
{
    private readonly VerificationTokenConfig _verificationTokenConfig;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ICacheManager _cacheManager;

    public VerificationTokenGenerator(IOptions<VerificationTokenConfig> verificationTokenConfig,
        ITokenGenerator tokenGenerator, ICacheManager cacheManager)
    {
        _verificationTokenConfig = verificationTokenConfig.Value;
        _tokenGenerator = tokenGenerator;
        _cacheManager = cacheManager;
    }

    public async Task<VerificationToken> Generate(Guid id, string email, Uri responseAddress)
    {
        string token = _tokenGenerator.Generate(_verificationTokenConfig.TokenLength);

        VerificationToken verificationToken = new()
        {
            Id = id,
            HashedEmailAddress = email,
            Token = token,
            VerificationBaseUrl = _verificationTokenConfig.VerificationBaseUrl,
            CreatedAt = DateTime.Now,
            ExpiryDuration = _verificationTokenConfig.TokenExpiration,
            ResponseAddress = responseAddress
        };

        await _cacheManager.Add(id.ToString(), verificationToken, _verificationTokenConfig.TokenExpiration);
        return verificationToken;
    }
}