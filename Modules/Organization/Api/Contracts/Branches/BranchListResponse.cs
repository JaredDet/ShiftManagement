namespace ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;

public sealed record BranchListResponse(
    List<BranchResponse> branches
);