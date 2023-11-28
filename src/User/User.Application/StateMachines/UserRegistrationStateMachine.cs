using MassTransit;
using Shared.Commands;
using Shared.Events;
using User.Domain.States;

namespace User.Application.StateMachines;

public class UserRegistrationStateMachine : MassTransitStateMachine<UserRegistrationState>
{
    public Event<NewUserRegistered> NewUserRegistered { get; private set; } = null!;
    public Request<UserRegistrationState, CreateProfile, ProfileCreated> CreateProfile { get; private set; } = null!;

    public UserRegistrationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => NewUserRegistered, x => x.CorrelateById(context => context.Message.Id));
        Request(() => CreateProfile);

        Initially(
            When(NewUserRegistered)
                .Then(x => x.Saga.RegisteredAt = x.Message.RegisteredAt)
                .Request(CreateProfile, context => new CreateProfile
                {
                    Id = context.Message.Id,
                    Username = context.Message.Username
                })
                .TransitionTo(CreateProfile.Pending)
        );
    }
}