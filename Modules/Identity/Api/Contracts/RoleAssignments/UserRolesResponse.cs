namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;

public sealed record UserRolesResponse
{
    public List<UserRoleResponse> Roles { get; set; } = new();
}