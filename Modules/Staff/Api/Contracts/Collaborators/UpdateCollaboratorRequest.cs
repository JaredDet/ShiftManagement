using ShiftManagement.Api.Validations;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.Collaborators;

public sealed record UpdateCollaboratorRequest
{
    [NotEmptyGuid]
    public Guid MainBranchId { get; set; }
    [NotEmptyGuid]
    public Guid MainPositionId { get; set; }
}