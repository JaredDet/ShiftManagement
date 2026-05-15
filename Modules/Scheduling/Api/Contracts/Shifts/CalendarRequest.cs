namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record CalendarRequest
{
    public Guid BranchId { get; init; }
    public DateOnly StartsOn { get; init; }
    public DateOnly EndsOn { get; init; }
}