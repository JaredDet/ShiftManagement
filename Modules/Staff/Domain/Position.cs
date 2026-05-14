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

    public List<EmploymentAssignment> Assignments { get; private set; } = new();

    private Position() { }

    public Position(
        Guid id,
        Guid companyId,
        string name,
        string? description,
        DateTime createdAt
    )
    {
        Id = id;
        CompanyId = companyId;
        Name = name;
        Description = description;

        CreatedAt = createdAt;
        Status = PositionStatus.Active;
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

    public void AddAssignment(EmploymentAssignment assignment)
    {
        Assignments.Add(assignment);
    }

    public void RemoveAssignment(EmploymentAssignment assignment)
    {
        Assignments.Remove(assignment);
    }
}