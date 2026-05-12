namespace ShiftManagement.Api.Modules.Organization.Api.Contracts;

public sealed record BranchListResponse(
    List<BranchResponse> branches
);