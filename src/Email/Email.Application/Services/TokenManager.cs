using Email.Application.Services.Interfaces;

namespace Email.Application.Services;

public class TokenManager
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ICacheManager _cacheManager;

    public TokenManager(ITokenGenerator tokenGenerator, ICacheManager cacheManager)
    {
        _tokenGenerator = tokenGenerator;
        _cacheManager = cacheManager;
    }
}