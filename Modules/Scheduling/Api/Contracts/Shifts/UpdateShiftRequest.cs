using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record UpdateShiftRequest
{

    [NotEmptyGuid]
    public Guid BranchId { get; init; }

    [NotEmptyGuid]
    public Guid PositionId { get; init; }
    public DateTime StartsAt { get; init; }

    [DateRange(nameof(StartsAt))]
    public DateTime EndsAt { get; init; }
}