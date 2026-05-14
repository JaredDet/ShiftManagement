using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Identity.Domain;

[Table("user_credentials")]
public class UserCredential
{
    [Key]
    public Guid UserId { get; private set; }

    public string PasswordHash { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private UserCredential() { }

    public UserCredential(
        Guid userId,
        string passwordHash
    )
    {
        UserId = userId;

        PasswordHash = passwordHash;

        CreatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(
        string newPasswordHash
    )
    {
        PasswordHash = newPasswordHash;
    }
}