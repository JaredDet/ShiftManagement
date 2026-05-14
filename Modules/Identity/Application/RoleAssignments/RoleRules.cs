using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Application.RoleAssignments;

public static class RoleRules
{
    public static bool RequiresBranch(Role role)
        => role == Role.Staff;

    public static bool DisallowsBranch(Role role)
        => role == Role.CompanyAdmin;
}