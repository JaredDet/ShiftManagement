using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;

public sealed record UserRoleResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Role Role { get; set; }
    public Guid? BranchId { get; set; }
    public DateTime CreatedAt { get; set; }
}