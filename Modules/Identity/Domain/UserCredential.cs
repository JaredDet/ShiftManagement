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

    private UserCredential(
        Guid userId,
        string passwordHash,
        DateTime createdAt
    )
    {
        UserId = userId;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    public static UserCredential Create(
        Guid userId,
        string passwordHash
    )
    {
        return new UserCredential(
            userId,
            passwordHash,
            DateTime.UtcNow
        );
    }

    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
}