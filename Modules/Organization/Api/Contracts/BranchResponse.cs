namespace ShiftManagement.Api.Modules.Organization.Api.Contracts;

public sealed record BranchResponse(
    Guid Id,
    Guid CompanyId,
    string Name,
    string Address,
    string Status,
    DateTime CreatedAt
);