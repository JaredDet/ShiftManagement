namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record CalendarResponse
{
    public Guid BranchId { get; init; }
    public string BranchName { get; init; }
    public DateOnly StartsOn { get; init; }
    public DateOnly EndsOn { get; init; }
    public List<ShiftResponse> Shifts { get; init; }
}