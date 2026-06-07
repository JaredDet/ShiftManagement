using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Staff.Domain;

[Table("positions")]
public class Position
{
    [Key]
    public Guid Id { get; private set; }

    public Guid CompanyId { get; private set; }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public PositionStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Position() { }

    private Position(
        Guid id,
        Guid companyId,
        string name,
        string? description,
        PositionStatus status,
        DateTime createdAt
    )
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
    }

    public static Position Create(
        Guid companyId,
        string name,
        string? description
    )
    {
        return new Position(
            Guid.NewGuid(),
            companyId,
            name,
            description,
            PositionStatus.Active,
            DateTime.UtcNow
        );
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public void Activate()
    {
        Status = PositionStatus.Active;
    }

    public void Deactivate()
    {
        Status = PositionStatus.Inactive;
    }
}