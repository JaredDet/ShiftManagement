using ShiftManagement.Api.Modules.Contracts.Domain;

namespace ShiftManagement.Api.Modules.Contracts.Api.Contracts.Query;

public sealed record MyEmploymentContractResponse
{
    public Guid Id { get; init; }

    public Guid CollaboratorId { get; init; }

    public string Type { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public string WorkScheduleType { get; init; } = string.Empty;

    public decimal SalaryAmount { get; init; }

    public string Currency { get; init; } = string.Empty;

    public string PayPeriod { get; init; } = string.Empty;

    public DateOnly StartDate { get; init; }

    public DateOnly? EndDate { get; init; }
}