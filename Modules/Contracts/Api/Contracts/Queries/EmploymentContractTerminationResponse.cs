namespace ShiftManagement.Api.Modules.Contracts.Api.Contracts.Queries;

public sealed record EmploymentContractTerminationResponse
{
    public Guid ApprovedByUserId { get; init; }

    public string ApprovedByName { get; init; } = string.Empty;

    public string Reason { get; init; } = string.Empty;

    public DateOnly TerminationDate { get; init; }

    public DateTime? TerminatedAt { get; init; }

    public string? Comment { get; init; }
}