using MassTransit;

namespace User.Domain.States;

public class UserRegistrationState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = null!;
    public DateTime RegisteredAt { get; set; }
    public int Version { get; set; }
}