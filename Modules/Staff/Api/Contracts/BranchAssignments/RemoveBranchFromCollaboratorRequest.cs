using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;

public sealed record RemoveBranchFromCollaboratorRequest
{
    [NotEmptyGuid]
    public Guid BranchId { get; set; }
}