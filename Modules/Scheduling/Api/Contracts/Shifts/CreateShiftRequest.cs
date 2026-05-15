namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record CreateShiftRequest
{
    public Guid BranchId { get; init; }
    public Guid PositionId { get; init; }
    public DateTime StartsAt { get; init; }
    public DateTime EndsAt { get; init; }
}