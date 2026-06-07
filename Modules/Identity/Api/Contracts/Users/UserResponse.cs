namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.Users;

public sealed record UserResponse(
    Guid Id,
    Guid CompanyId,

    string Name,
    string Email,

    string Status,

    DateTime CreatedAt
);