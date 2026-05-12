using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Organization.Domain;

[Table("branches")]
public sealed class Branch
{
    [Key]
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string Name { get; private set; }

    public string Address { get; private set; }

    public BranchStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Branch()
    {
    }

    public Branch(
        Guid id,
        Guid companyId,
        string name,
        string address,
        BranchStatus status,
        DateTime createdAt
    )
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Address = address;
        Status = status;
        CreatedAt = createdAt;
    }

    public void Update(
        string name,
        string address
    )
    {
        Name = name;
        Address = address;
    }

    public void Deactivate()
    {
        Status = BranchStatus.Inactive;
    }
}