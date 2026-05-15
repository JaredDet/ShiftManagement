using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Scheduling.Api.Contracts;

public sealed record AssignCollaboratorToShiftRequest
{
    [NotEmptyGuid]
    public Guid ShiftId { get; init; }

    [NotEmptyGuid]
    public Guid CollaboratorId { get; init; }
}