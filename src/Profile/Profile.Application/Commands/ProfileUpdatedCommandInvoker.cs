using Profile.Domain.Entities;

namespace Profile.Application.Commands;

public class ProfileUpdatedCommandInvoker
{
    private readonly Dictionary<string, IProfileUpdatedCommand> _commands = new()
    {
        { "Email", new EmailUpdatedCommand() },
        { "Phone", new PhoneUpdatedCommand() }
    };

    public async Task ExecuteCommands(UserProfile userProfile, UserProfile updatedProfile)
    {
        foreach (var propertyInfo in typeof(UserProfile).GetProperties())
        {
            var propertyName = propertyInfo.Name;
            if (_commands.TryGetValue(propertyName, out var command))
            {
                await command.ExecuteAsync(userProfile, updatedProfile);
            }
        }
    }
}