using MassTransit;
using Shared.Commands;
using Shared.Events;
using User.Domain.States;

namespace User.Application.StateMachines;

public class UserRegistrationStateMachine : MassTransitStateMachine<UserRegistrationState>
{
    public UserRegistrationStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => NewUserRegistered);

        Request(() => CreateProfile, r =>
        {
            r.Timeout = TimeSpan.FromSeconds(10);
        });
        Request(() => VerifyEmail, r =>
        {
            r.Timeout = TimeSpan.Zero;
        });

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
                .TransitionTo(ProfilePendingSubmission),
            When(CreateProfile.Faulted)
                .TransitionTo(Failed),
            When(CreateProfile.TimeoutExpired)
                .TransitionTo(Failed)
        );
        During(ProfilePendingSubmission,
            When(ProfileSubmitted)
                .Request(VerifyEmail, context => new SendVerificationEmail()
                {
                    Email = context.Message.Email,
                    FullName = context.Message.FullName
                })
                .TransitionTo(VerifyEmail.Pending)
        );
        During(VerifyEmail.Pending,
            When(VerifyEmail.Completed)
                .TransitionTo(Verified),
            When(VerifyEmail.Faulted)
                .TransitionTo(Failed),
            When(VerifyEmail.TimeoutExpired)
                .TransitionTo(Failed)
        );
    }

    public State ProfilePendingSubmission { get; set; } = null!;
    public State Verified { get; set; } = null!;
    public State Failed { get; set; } = null!;
    public Event<NewUserRegistered> NewUserRegistered { get; set; } = null!;
    public Event<ProfileSubmitted> ProfileSubmitted { get; set; } = null!;
    public Request<UserRegistrationState, CreateProfile, ProfileCreated> CreateProfile { get; set; } = null!;
    public Request<UserRegistrationState, SendVerificationEmail, EmailVerified> VerifyEmail { get; set; } = null!;
}