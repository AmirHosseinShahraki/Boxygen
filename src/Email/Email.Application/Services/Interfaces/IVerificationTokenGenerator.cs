using Email.Domain;

namespace Email.Application.Services.Interfaces;

public interface IVerificationTokenGenerator
{
    public Task<VerificationToken> Generate(Guid id, string email, Uri responseAddress);
}