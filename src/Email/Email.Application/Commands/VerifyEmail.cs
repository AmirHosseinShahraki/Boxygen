namespace Email.Application.Commands;

public record VerifyEmail(Guid Id, string Email, string Token);