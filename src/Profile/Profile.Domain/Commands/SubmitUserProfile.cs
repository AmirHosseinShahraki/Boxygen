using Profile.Domain.Entities;

namespace Profile.Domain.Commands;

public class SubmitUserProfile
{
    public Guid UserProfileId { get; set; }
    public UserProfile Profile { get; set; } = null!;
}