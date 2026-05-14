namespace ShiftManagement.Api.Modules.Staff.Api.Contracts.BranchAssignments;

public sealed record CollaboratorBranchesResponse
{
    public List<CollaboratorBranchResponse> Branches { get; set; } = [];
}