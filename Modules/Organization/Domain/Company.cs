using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Organization.Domain;

[Table("companies")]
public sealed class Company
{
    [Key]
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public CompanyStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Company()
    {
    }

    public Company(
        Guid id,
        string name,
        CompanyStatus status,
        DateTime createdAt
    )
    {
        Id = id;
        Name = name;
        Status = status;
        CreatedAt = createdAt;
    }

    public void Update(string name)
    {
        Name = name;
    }

    public void Deactivate()
    {
        Status = CompanyStatus.Inactive;
    }
}