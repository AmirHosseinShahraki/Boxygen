using MassTransit;

namespace Shared.Events;

public record ProfileCreated : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public DateTime CreatedAt { get; set; }
}