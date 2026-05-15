using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record CalendarRequest
{

    [NotEmptyGuid]
    public Guid BranchId { get; init; }
    public DateOnly StartsAt { get; init; }

    [DateRange(nameof(StartsAt))]
    public DateOnly EndsAt { get; init; }
}