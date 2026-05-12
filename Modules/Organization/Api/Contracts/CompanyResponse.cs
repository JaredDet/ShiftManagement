namespace ShiftManagement.Api.Modules.Organization.Api.Contracts;

public sealed record CompanyResponse(
    Guid Id,
    string Name,
    string Status,
    DateTime CreatedAt
);