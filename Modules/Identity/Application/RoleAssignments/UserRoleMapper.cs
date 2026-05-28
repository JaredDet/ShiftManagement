using ShiftManagement.Api.Modules.Identity.Domain;
using ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public static class UserRoleMapper
{
    public static UserRoleResponse ToResponse(UserRole role)
    {
        return new UserRoleResponse
        {
            Id = role.Id,
            UserId = role.UserId,
            Role = role.Role.ToString(),
            BranchId = role.BranchId,
            CreatedAt = role.CreatedAt
        };
    }
}