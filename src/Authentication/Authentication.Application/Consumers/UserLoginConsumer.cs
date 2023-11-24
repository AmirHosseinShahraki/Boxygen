using Authentication.Application.Services;
using Authentication.Domain.Commands;
using Authentication.Domain.Messages;
using Authentication.Domain.Repositories;
using BCrypt.Net;
using MassTransit;

namespace Authentication.Application.Consumers;

public class UserLoginConsumer : IConsumer<LoginUser>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public UserLoginConsumer(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task Consume(ConsumeContext<LoginUser> context)
    {
        var user = await _userRepository.GetUserByUsername(context.Message.Username);

        if (user is null || !BC.EnhancedVerify(context.Message.Password, user.Password, HashType.SHA512))
        {
            await context.RespondAsync(new AuthFailed()
            {
                Username = context.Message.Username
            });
            return;
        }

        var authToken = _jwtService.GenerateToken(user.Username);

        await context.RespondAsync(authToken);
    }
}