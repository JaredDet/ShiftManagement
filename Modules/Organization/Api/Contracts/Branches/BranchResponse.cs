namespace ShiftManagement.Api.Modules.Organization.Api.Contracts.Branches;

public sealed record BranchResponse(
    Guid Id,
    Guid CompanyId,
    string Name,
    string Address,
    string Status,
    DateTime CreatedAt
);