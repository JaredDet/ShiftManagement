using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;

public sealed record AssignPositionToCollaboratorRequest
{
    [NotEmptyGuid]
    public Guid PositionId { get; set; }
}