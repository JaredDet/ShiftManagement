namespace ShiftManagement.Api.Modules.Contracts.Application;

public sealed record EmploymentContractPdfModel
{
    public Guid Id { get; init; }

    public string CollaboratorName { get; init; } = string.Empty;
    public string ApprovedByName { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;

    public string WorkScheduleType { get; init; } = string.Empty;

    public decimal SalaryAmount { get; init; }
    public string Currency { get; init; } = string.Empty;
    public string PayPeriod { get; init; } = string.Empty;

    public DateOnly StartsAt { get; init; }
    public DateOnly? EndsAt { get; init; }

    public string? TerminationReason { get; init; }
    public DateTime CreatedAt { get; init; }
}