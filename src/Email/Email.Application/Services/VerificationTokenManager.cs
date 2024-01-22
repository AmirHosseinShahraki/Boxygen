using Email.Application.Services.Interfaces;
using Email.Domain;

namespace Email.Application.Services;

public class VerificationTokenManager : IVerificationTokenManager
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ICacheManager _cacheManager;

    public VerificationTokenManager(ITokenGenerator tokenGenerator, ICacheManager cacheManager)
    {
        _tokenGenerator = tokenGenerator;
        _cacheManager = cacheManager;
    }

    public async Task<VerificationToken> Generate(string email)
    {
        string token = _tokenGenerator.Generate(32);
        var expirationTimeSpan = TimeSpan.FromMinutes(20);

        var verificationToken = new VerificationToken(email, token, expirationTimeSpan);

        await _cacheManager.Add(token, verificationToken, expirationTimeSpan);
        return verificationToken;
    }
}