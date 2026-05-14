using System.ComponentModel.DataAnnotations;
using ShiftManagement.Api.Modules.Identity.Domain;

namespace ShiftManagement.Api.Modules.Identity.Api.Contracts.RoleAssignments;

public sealed record RemoveRoleFromUserRequest
{
    [Required]
    public Role Role { get; set; }

    public Guid? BranchId { get; set; }
}