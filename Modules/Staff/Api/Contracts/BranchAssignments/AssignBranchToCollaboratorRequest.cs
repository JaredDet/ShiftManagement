using ShiftManagement.Api.Shared;

namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;

public sealed record AssignBranchToCollaboratorRequest
{
    [NotEmptyGuid]
    public Guid BranchId { get; init; }
}