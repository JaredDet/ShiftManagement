using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftManagement.Api.Modules.Staff.Domain;

[Table("employment_assignments")]
public class EmploymentAssignment
{
    [Key]
    public Guid Id { get; private set; }

    public Guid EmployeeId { get; private set; }

    public Guid ReferenceId { get; private set; }

    public AssignmentType Type { get; private set; }

    public bool IsPrimary { get; private set; }

    public AssignmentStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Employee Employee { get; private set; } = null!;

    private EmploymentAssignment() { }

    public static EmploymentAssignment Create(
        Guid employeeId,
        Guid referenceId,
        AssignmentType type,
        bool isPrimary,
        DateTime createdAt
    )
    {
        return new EmploymentAssignment(
            Guid.NewGuid(),
            employeeId,
            referenceId,
            type,
            isPrimary,
            createdAt
        );
    }

    private EmploymentAssignment(
        Guid id,
        Guid employeeId,
        Guid referenceId,
        AssignmentType type,
        bool isPrimary,
        DateTime createdAt
    )
    {
        Id = id;
        EmployeeId = employeeId;
        ReferenceId = referenceId;
        Type = type;
        IsPrimary = isPrimary;
        CreatedAt = createdAt;
        Status = AssignmentStatus.Active;
    }

    public void MarkAsPrimary()
    {
        IsPrimary = true;
    }

    public void UnmarkAsPrimary()
    {
        IsPrimary = false;
    }

    public void Activate() => Status = AssignmentStatus.Active;

    public void Deactivate() => Status = AssignmentStatus.Inactive;
}