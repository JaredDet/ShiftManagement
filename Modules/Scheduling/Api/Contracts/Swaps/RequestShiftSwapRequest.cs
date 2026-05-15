using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record RequestShiftSwapRequest
{
    [NotEmptyGuid]
    public Guid SourceShiftId { get; init; }

    [NotEmptyGuid]
    public Guid TargetShiftId { get; init; }

    [NotEmptyGuid]
    public Guid TargetCollaboratorId { get; init; }
}