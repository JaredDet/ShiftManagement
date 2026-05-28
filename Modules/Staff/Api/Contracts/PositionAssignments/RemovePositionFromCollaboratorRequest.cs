using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.PositionAssignments;

public sealed record RemovePositionFromCollaboratorRequest
{
    [NotEmptyGuid]
    public Guid PositionId { get; set; }
}