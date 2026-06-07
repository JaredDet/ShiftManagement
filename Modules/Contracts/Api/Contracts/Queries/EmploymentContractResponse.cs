namespace ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;

public sealed record EmploymentContractResponse
{
    public Guid Id { get; init; }

    public Guid CollaboratorId { get; init; }
    public string CollaboratorName { get; init; } = string.Empty;

    public Guid ApprovedByUserId { get; init; }
    public string ApprovedByName { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;

    public string WorkScheduleType { get; init; } = string.Empty;

    public decimal SalaryAmount { get; init; }

    public string Currency { get; init; } = string.Empty;
    public string PayPeriod { get; init; } = string.Empty;

    public DateOnly StartsAt { get; init; }
    public DateOnly? EndsAt { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime? TerminatedAt { get; init; }

    public EmploymentContractTerminationResponse? Termination { get; init; }
}