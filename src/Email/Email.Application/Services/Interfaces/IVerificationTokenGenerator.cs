using Email.Domain;

namespace Email.Application.Services.Interfaces;

public interface IVerificationTokenGenerator
{
    public Task<VerificationToken> Generate(string email);
}