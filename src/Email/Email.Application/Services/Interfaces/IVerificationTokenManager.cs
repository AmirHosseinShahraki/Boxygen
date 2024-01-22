using Email.Domain;

namespace Email.Application.Services.Interfaces;

public interface IVerificationTokenManager
{
    public Task<VerificationToken> Generate(string email);
}