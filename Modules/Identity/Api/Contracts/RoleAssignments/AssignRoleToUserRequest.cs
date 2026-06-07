using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;

public sealed record AssignRoleToUserRequest
{
    [Required]
    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }

    public Guid? BranchId { get; set; }
}