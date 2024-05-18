using System.Reflection;
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
        foreach (PropertyInfo propertyInfo in typeof(UserProfile).GetProperties())
        {
            string propertyName = propertyInfo.Name;
            if (_commands.TryGetValue(propertyName, out IProfileUpdatedCommand? command))
            {
                await command.ExecuteAsync(userProfile, updatedProfile);
            }
        }
    }
}