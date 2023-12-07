using MassTransit;

namespace Shared.Events;

public record ProfileSubmitted : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
}