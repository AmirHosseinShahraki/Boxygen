using MassTransit;

namespace Shared.Events;

public record ProfileSubmitted : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}