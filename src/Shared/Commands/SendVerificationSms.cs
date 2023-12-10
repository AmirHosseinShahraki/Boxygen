using MassTransit;

namespace Shared.Commands;

public record SendVerificationSms : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string Phone { get; set; } = null!;
}