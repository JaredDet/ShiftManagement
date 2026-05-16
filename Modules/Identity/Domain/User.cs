using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShiftManagement.Api.Modules.Identity.Domain;

[Index(nameof(Email), IsUnique = true)]
[Table("users")]
public class User
{
    [Key]
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public UserStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private User() { }

    private User(
        Guid id,
        Guid companyId,
        string name,
        string email,
        UserStatus status,
        DateTime createdAt
    )
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Email = email;
        Status = status;
        CreatedAt = createdAt;
    }

    public static User Create(
        Guid companyId,
        string name,
        string email
    )
    {
        return new User(
            Guid.NewGuid(),
            companyId,
            name,
            email,
            UserStatus.Active,
            DateTime.UtcNow
        );
    }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public void Deactivate()
    {
        Status = UserStatus.Inactive;
    }
}