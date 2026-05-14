using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Identity.Domain;

[Table("user_roles")]
public class UserRole
{
    [Key]
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public Role Role { get; private set; }

    public Guid? BranchId { get; private set; }

    public UserRoleStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private UserRole() { }

    public UserRole(Guid userId, Role role, Guid? branchId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Role = role;
        BranchId = branchId;
        Status = UserRoleStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = UserRoleStatus.Inactive;
    }
}