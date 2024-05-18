using Email.Application.Services.Interfaces;
using Email.Domain;

namespace Email.Infrastructure.Services.TokenValidator;

public class VerificationTokenValidator : IVerificationTokenValidator
{
    private readonly ICacheManager _cacheManager;

    public VerificationTokenValidator(ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    public async Task<VerificationToken?> Validate(Guid id, string email, string token)
    {
        VerificationToken? verificationToken = await _cacheManager.Get<VerificationToken>(id.ToString());
        if (verificationToken is null)
        {
            return null;
        }

        if (!verificationToken.IsHashedEmailEqual(email))
        {
            return null;
        }

        if (verificationToken.IsExpired())
        {
            await _cacheManager.Remove(id.ToString());
            return null;
        }

        if (token != verificationToken.Token)
        {
            return null;
        }

        await _cacheManager.Remove(id.ToString());
        return verificationToken;
    }
}