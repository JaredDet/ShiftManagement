namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record ShiftResponse
{
    public Guid Id { get; init; }
    public Guid BranchId { get; init; }
    public string BranchName { get; init; }
    public Guid PositionId { get; init; }
    public string PositionName { get; init; }

    public DateTime StartsAt { get; init; }
    public DateTime EndsAt { get; init; }

    public string Status { get; init; }
    public DateTime CreatedAt { get; init; }
}