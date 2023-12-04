using MassTransit;
using Shared.Commands;
using Shared.Events;
using User.Domain.States;

namespace User.Application.StateMachines;

public class UserRegistrationStateMachine : MassTransitStateMachine<UserRegistrationState>
{
    public State Failed { get; set; } = null!;
    public Event<NewUserRegistered> NewUserRegistered { get; set; } = null!;
    public Request<UserRegistrationState, CreateProfile, ProfileCreated> CreateProfile { get; set; } = null!;

    public UserRegistrationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => NewUserRegistered);

        Request(() => CreateProfile, r => { r.Timeout = TimeSpan.FromSeconds(2); });

        Initially(
            When(NewUserRegistered)
                .Then(x => x.Saga.RegisteredAt = x.Message.RegisteredAt)
                .Request(CreateProfile, context => new CreateProfile
                {
                    CorrelationId = context.Message.CorrelationId
                })
                .TransitionTo(CreateProfile.Pending)
        );
        During(CreateProfile.Pending,
            When(CreateProfile.Completed)
                .Finalize(),
            When(CreateProfile.Faulted)
                .TransitionTo(Failed),
            When(CreateProfile.TimeoutExpired)
                .TransitionTo(Failed)
        );
    }
}