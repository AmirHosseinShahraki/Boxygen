using MassTransit;

namespace Shared.Events;

public record NewUserRegistered : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string Username { get; set; } = null!;
    public DateTime RegisteredAt { get; set; }
}