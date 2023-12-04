using MassTransit;

namespace Shared.Commands;

public record CreateProfile : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
}