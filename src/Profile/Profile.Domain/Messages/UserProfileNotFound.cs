namespace Profile.Domain.Messages;

public record UserProfileNotFound
{
    public Guid Id { get; set; }
}