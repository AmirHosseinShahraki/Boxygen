using Profile.Domain.Entities;

namespace Profile.Domain.Commands;

public record UpdateUserProfile
{
    public Guid UserProfileId { get; set; }
    public UserProfile Profile = null!;
};