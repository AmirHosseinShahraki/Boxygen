using MassTransit;

namespace Shared.Events;

public record PhoneVerified : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}