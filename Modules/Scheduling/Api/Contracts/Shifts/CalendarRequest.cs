using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record CalendarRequest
{

    [NotEmptyGuid]
    public Guid BranchId { get; init; }
    public DateTime StartsAt { get; init; }

    [DateRange(nameof(StartsAt))]
    public DateTime EndsAt { get; init; }
}