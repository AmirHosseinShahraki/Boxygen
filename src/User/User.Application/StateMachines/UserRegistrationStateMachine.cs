using MassTransit;
using Shared.Events;
using User.Domain.States;

namespace User.Application.StateMachines;

public class UserRegistrationStateMachine : MassTransitStateMachine<UserRegistrationState>
{
    public State Registered { get; private set; } = null!;

    public Event<NewUserRegistered> NewUserRegistered { get; private set; } = null!;

    public UserRegistrationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => NewUserRegistered, x => x.CorrelateById(context => context.Message.Id));

        Initially(
            When(NewUserRegistered)
                .TransitionTo(Registered)
        );
    }
}