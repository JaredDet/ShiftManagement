namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record CalendarResponse
{
    public Guid BranchId { get; init; }
    public string BranchName { get; init; }
    public DateTime StartsAt { get; init; }
    public DateTime EndsAt { get; init; }
    public List<ShiftResponse> Shifts { get; init; }
}