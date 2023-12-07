using MassTransit;

namespace Shared.Events;

public record EmailVerified : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}