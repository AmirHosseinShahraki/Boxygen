using Email.Domain;

namespace Email.Application.Services.Interfaces;

public interface IVerificationTokenValidator
{
    public Task<VerificationToken?> Validate(Guid id, string email, string token);
}