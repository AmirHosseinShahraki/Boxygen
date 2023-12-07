using MassTransit;

namespace Shared.Commands;

public record SendVerificationEmail : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
}